using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Fire_Rework : MonoBehaviour {

    private Rigidbody rigidBody;

    GameObject CandleStick;//燭台の当たり判定取得
    private CenterCollision CandleTrigger;

    private int ClickFlg = 99;//クリックしているかどうか
    private bool IsJump = false;//
    private bool FirstVelocity = true;//一度だけ入る(1フレーム目

    private float Speed;//移動速度

    private float x, y, z;

    private Vector3 Force;
    private float Force_y;//上に与える力
    private float FreeFallGrvity;//フレーム後に与える力
    private float UnnaturalGrvity;//指を離した時に与える力
    private int FrameCount;

    private Vector3 StartPosition;//初期位置
    public GameObject Camera;
    private Vector3 CameraPosition;

    private float Length;//半径
    float AtanAngle;//方位角　角度
    float count;
    [SerializeField] private float RotateSpeed;

    [SerializeField] private Vector3 Vel;

    Transform ChildObj;
    private bool HitCandle = false;
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //CandleTrigger = CandleStick.GetComponent<CenterCollision>();

        StartPosition = this.transform.position;
        CameraPosition = Camera.transform.position;

        Speed = 1.0f;
        Force_y = 20.0f;

        FreeFallGrvity = 9.8f;
        UnnaturalGrvity = 19.6f;

        FrameCount = 0;

        Length = transform.position.magnitude - 0.5f;
        AtanAngle = Mathf.Atan2(StartPosition.x, StartPosition.z);
        count = AtanAngle;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        InputMouse_Touch();
        
        if(ClickFlg == 2)
            FrameCount++;
        
    }
    private void FixedUpdate()
    {
        RotateFire();
        Debug.Log(count);
    }

    void InputMouse_Touch()
    {
        // エディタ、実機で処理を分ける
        if (Application.isEditor)
        {// エディタで実行中
            if (Input.GetMouseButtonDown(0))
            {//押した時
                Debug.Log("1");
            }
            if (Input.GetMouseButton(0))
            {//押し続けた時
                ClickFlg = 2;
                Debug.Log("2");
            }
            if (Input.GetMouseButtonUp(0))
            {//離した時
                ClickFlg = 0;
            }
        }
        else
        {
            // タッチされているかチェック
            if (Input.touchCount > 0)
            {
                // タッチ情報の取得
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("押した瞬間");
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    ClickFlg = 0;
                    Debug.Log("離した瞬間");
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    ClickFlg = 2;
                    Debug.Log("押しっぱなし");
                }
            }
        }
    }

    void RotateFire()
    {
        if (ClickFlg == 0) {
            if (FirstVelocity) {
                FirstVelocity = false;
            }
            count += Time.deltaTime * RotateSpeed;

            //円運動　
            x = Length * Mathf.Sin(count);
            y = transform.position.y;
            z = Length * Mathf.Cos(count);
            transform.position = new Vector3(x, y, z);

            Force_y = Force_y - UnnaturalGrvity;

            Force = new Vector3(0, Force_y, 0);
            rigidBody.AddForce(Force);
            FrameCount = 0;
        }

        if (ClickFlg == 2) {
            count += Time.deltaTime * RotateSpeed;
            if (FirstVelocity) {//一度だけ入る
                rigidBody.velocity = Vel;
                FirstVelocity = false;
            }
            if (FrameCount > 20) {//30フレーム超えたらForceに重力を加算
                Force_y = Force_y - FreeFallGrvity;
            }
            //円運動　
            x = Length * Mathf.Sin(count);
            y = transform.position.y;
            z = Length * Mathf.Cos(count);
            transform.position = new Vector3(x, y, z);
            //Debug.Log(transform.position);
            Force = new Vector3(0, Force_y, 0);
            rigidBody.AddForce(Force);
        }

        if (ClickFlg == 0 && CandleTrigger.trigger == true) {
            Debug.Log("クリックしていない&&地面についている");
            Force_y = 20.0f;
            ClickFlg = 99;
            FirstVelocity = true;
            //rigidBody.velocity = Vector3.zero;
            //HitCandle = false;
        }

        if (ClickFlg == 99) {
            //rigidBody.velocity = Vector3.zero;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Candle") {
            //HitCandle = true;
            ChildObj = other.transform.Find("Lamp");
            CandleTrigger = ChildObj.GetComponent<CenterCollision>();
            Debug.Log("CandleTrigger" + CandleTrigger.trigger);
            Debug.Log(ChildObj);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn") {
            ClickFlg = 99;
            rigidBody.velocity = Vector3.zero;
            Force_y = 20.0f;
            FirstVelocity = true;
            count = AtanAngle;
            transform.position = StartPosition;
            Debug.Log(StartPosition);
        }
    }
}

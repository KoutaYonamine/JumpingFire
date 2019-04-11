using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Fire_Rework : MonoBehaviour {

    private Rigidbody rigidBody;

    public GameObject CandleStick;//燭台の当たり判定取得
    private CenterCollision CandleTrigger;

    private int ClickFlg = 99;//クリックしているかどうか
    private bool IsJump = false;
    private bool InitialVelocity = true;

    private float Speed;
    private float Radius;

    private float x, y, z;

    private Vector3 Force;
    private float Force_y;
    private float FreeFallGrvity;
    private float UnnaturalGrvity;
    private int FrameCount;

    private Vector3 StartPosition;
    public GameObject Camera;
    private Vector3 CameraPosition;

    private float Length;
    float AtanAngle;
    float count;


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        CandleTrigger = CandleStick.GetComponent<CenterCollision>();

        StartPosition = this.transform.position;
        CameraPosition = Camera.transform.position;

        Speed = 1.0f;
        Radius = 30.0f;
        Force_y = 20.0f;

        FreeFallGrvity = 0.98f;
        UnnaturalGrvity = 2.98f;
        FrameCount = 0;

        Length = transform.position.magnitude;
        AtanAngle = Mathf.Atan2(StartPosition.x, StartPosition.z);
        count = AtanAngle;
        Debug.Log(AtanAngle);
        Debug.Log(count);
    }
    
    // Update is called once per frame
    void Update()
    {
        InputMouse_Touch();
        
        if(ClickFlg == 2)
            FrameCount++;
        Debug.Log(ClickFlg);
    }
    private void FixedUpdate()
    {
        RotateFire();
    }

    void InputMouse_Touch()
    {
        // エディタ、実機で処理を分ける
        if (Application.isEditor) {// エディタで実行中
            if (Input.GetMouseButtonDown(0)) {//押した時
                Debug.Log("1");
            }
            if (Input.GetMouseButton(0)) {//押し続けた時
                ClickFlg = 2;
                Debug.Log("2");
            }
            if (Input.GetMouseButtonUp(0)) {//離した時
                ClickFlg = 0;
            }
        }
    }

    void RotateFire()
    {
        x = Length * Mathf.Sin(count * Speed);
        y = transform.position.y;
        z = Length * Mathf.Cos(count * Speed);
        transform.position = new Vector3(x, y, z);
        Debug.Log(transform.position);

        if (InitialVelocity) {
            InitialVelocity = false;
        }

        if (ClickFlg == 0) {
            if (InitialVelocity) {
                InitialVelocity = false;
            }
            count += 1 * Time.deltaTime;

            //円運動　
            x = Length * Mathf.Sin(count * Speed);
            y = transform.position.y;
            z = Length * Mathf.Cos(count * Speed);
            transform.position = new Vector3(x, y, z);

            Force_y = Force_y - UnnaturalGrvity;

            Force = new Vector3(0, Force_y, 0);
            rigidBody.AddForce(Force);

            FrameCount = 0;


        }
        
        if (ClickFlg == 2) {
            count += 1 * Time.deltaTime;

            //円運動　
            x = Length * Mathf.Sin(count * Speed);
            y = transform.position.y;
            z = Length * Mathf.Cos(count * Speed);
            transform.position = new Vector3(x, y, z);
            Debug.Log("X : " + x + "   " + "Z : " + z);

            if (FrameCount > 30) {//30フレーム超えたらForceに重力を加算
                Force_y = Force_y - FreeFallGrvity;
            }

            Force = new Vector3(0, Force_y, 0);
            rigidBody.AddForce(Force);
        }

        if(ClickFlg == 0 && CandleTrigger.trigger == true) {
            Debug.Log("クリックしていない&&地面についている");
            Force_y = 20.0f;
            ClickFlg = 99;
            InitialVelocity = true;
        }
    }
}

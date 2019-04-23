using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player : MonoBehaviour {

    private Rigidbody rigidBody;

    private int ClickFlg = 99;//クリックしているかどうか
    private float NotBarrageCount = 0;//連打禁止   燭台に乗ったら入力受付
    private bool ReleasedFlg;//指を離したかどうか
    private bool FallFlg;//落ちたかどうか

    private bool FirstVelocity = true;//一度だけ入る(1フレーム目

    private bool AddSpeedFlg; //燭台の中心に当たったかどうか
    private bool Initialize; //スピード初期化


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
        Debug.Log(rigidBody.velocity);
        InputMouse_Touch();
        if(ClickFlg == 2)
            FrameCount++;
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
                ClickFlg = 2;
                ReleasedFlg = true;
            }
            if (Input.GetMouseButton(0)) {//押し続けた時
            }
            if (Input.GetMouseButtonUp(0)) {//離した時
                if (ReleasedFlg) {
                    ClickFlg = 0;
                    ReleasedFlg = false;
                }
            }
        }
        else
        {
            // タッチされているかチェック
            if (Input.touchCount > 0)
            {
                // タッチ情報の取得
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)//押した瞬間
                {
                    ClickFlg = 2;
                    ReleasedFlg = true;
                }

                if (touch.phase == TouchPhase.Moved) {//押しっぱなし
                }

                if (touch.phase == TouchPhase.Ended)//離した瞬間
                {
                    if (ReleasedFlg) {
                        ClickFlg = 0;
                        ReleasedFlg = false;
                    }
                }
            }
        }
    }

    void RotateFire()
    {
        if (ClickFlg == 0) {
            count += Time.deltaTime * RotateSpeed;
　
            CircularMotion();//円運動

            Force_y = Force_y - UnnaturalGrvity;//離した時に急な落下をさせる

            Force = new Vector3(0, Force_y, 0);
            rigidBody.AddForce(Force);
            //FrameCount = 0;//フレームカウントを初期化
        }

        if (ClickFlg == 2) {
            count += Time.deltaTime * RotateSpeed;//今いる位置から移動を開始
            if (FirstVelocity) {//一度だけ入る
                rigidBody.velocity = Vel;//初速度を与える
                FirstVelocity = false;
            }
            if (FrameCount > 20) {//20フレーム超えたらForceに重力を加算
                Force_y = Force_y - FreeFallGrvity;
            }

            CircularMotion();//円運動

            Force = new Vector3(0, Force_y, 0);//y座標に力を加算
            rigidBody.AddForce(Force);
        }

        if (Initialize == true)//燭台に乗った時
        {
            Force_y = 20.0f;//y軸に与える力を初期化
            FirstVelocity = true;//一度だけ入る処理をリセット
            rigidBody.isKinematic = false;
            //rigidBody.velocity = Vector3.zero;
            Initialize = false;
            ReleasedFlg = false;
            FrameCount = 0;//フレームカウントを初期化
            ClickFlg = 99;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn") {//地面に落ちたらスタートのポジションにリスポーン
            rigidBody.velocity = Vector3.zero;
            Force_y = 20.0f;//y軸に与える力を初期化
            count = AtanAngle;//円運動の始まりを初期化
            transform.position = StartPosition;//初期位置にセット
            FirstVelocity = true;//一度だけ入る処理をリセット
            ReleasedFlg = false;
            FrameCount = 0;//フレームカウントを初期化
            ClickFlg = 99;
        }

        if (collision.gameObject.tag == "Candle")
        {
        }
    }

    public bool addspeed//スピードの変化
    {
        get { return AddSpeedFlg ; }
        set { AddSpeedFlg = value; }
    }
    public bool initialize//燭台に乗ったかどうか
    {
        get { return Initialize; }
        set { Initialize = value; }
    }
  
    private void CircularMotion()//円運動
    {
        x = Length * Mathf.Sin(count);
        y = transform.position.y;
        z = Length * Mathf.Cos(count);
        transform.position = new Vector3(x, y, z);
    }
}

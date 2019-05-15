using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player_copy : InitializeVariable     //サブクラス
{
    private Rigidbody rigidBody;

    private Vector3 ClearDirection;//クリアの聖火台の方向

    private float x, y, z;//プレイヤーの移動座標

    private float FreeFallGrvity = 9.8f;//フレーム後に与える力
    private float UnnaturalGrvity = 19.6f;//指を離した時に与える力

    [SerializeField] private float AddSpeed;//燭台の中心に乗った時にSpeedUp
    private float tempRotateSpeed;//RotateSpeedの退避用変数

    private GameObject GOAL;

    private Stairscollision staircollision; //stairscollisionのスクリプト 変更点

    private float Length;//半径
    private float AtanAngle;//方位角　角度
    private float count;
    [SerializeField] Vector3 _Vel;
    [SerializeField] Vector3 ClearVelocity;//クリアの聖火台にジャンプする時のVelocity

    public Camera MainCamera;
    public Camera ClearCamera;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        FireWindZone.SetActive(false);//WindZoneを非アクティブに

        StartPosition = this.transform.position;
        //Camera = GameObject.Find("Main Camera");
        //CameraPosition = Camera.transform.position;

        Length = transform.position.magnitude - 0.5f;
        AtanAngle = Mathf.Atan2(StartPosition.x, StartPosition.z);
        count = AtanAngle;

        tempRotateSpeed = RotateSpeed;//RotateSpeedの値を退避

        staircollision = GetComponent<Stairscollision>();

        GOAL = GameObject.Find("PublishFire_Prefab (1)");
        GOAL.SetActive(false);

        ClearCamera.enabled = false;//クリア時のカメラを無効
    }

    // Update is called once per frame
    void Update()
    {
        InputMouse_Touch();

        if (ClickFlg == 2)
            FrameCount++;
    }
    private void FixedUpdate()
    {
        FireMovement();
    }

    void InputMouse_Touch()
    {
        // エディタ、実機で処理を分ける
        if (Application.isEditor) {// エディタで実行中
            if (Input.GetMouseButtonDown(0) && staircollision.getmoveflag() == true && staircollision.getmouseflag() == true && ClearInputFlg == true) {//押した時
                ClickFlg = 2;
                ReleasedFlg = true;
                BoundFlg = true;
            }
            if (Input.GetMouseButtonUp(0) && ClearInputFlg == true) {//離した時
                if (ReleasedFlg) {
                    ClickFlg = 0;
                    ReleasedFlg = false;
                    BoundFlg = true;
                }
            }
            if (Input.GetMouseButtonDown(0) && ClearInputFlg == false)
            {
                MovementToClear();
            }
        }
        else
        {
            // タッチされているかチェック
            if (Input.touchCount > 0)
            {
                // タッチ情報の取得
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && staircollision.getmoveflag() == true && staircollision.getmouseflag() == true && ClearInputFlg == true)//押した瞬間
                {
                    ClickFlg = 2;
                    ReleasedFlg = true;
                    BoundFlg = true;
                }

                if (touch.phase == TouchPhase.Ended && ClearInputFlg == true)//離した瞬間
                {
                    if (ReleasedFlg) {
                        ClickFlg = 0;
                        ReleasedFlg = false;
                        BoundFlg = true;
                    }
                }
                if (touch.phase == TouchPhase.Began && ClearInputFlg == false) {
                    MovementToClear();
                }
            }
        }
    }

    void FireMovement()//Playerの挙動
    {
        if (ClickFlg == 0) {
            count += Time.deltaTime * RotateSpeed;

            CircularMotion();//円運動
            
            Force_y = Force_y - UnnaturalGrvity;//離した時に急な落下をさせる

            Force = new Vector3(0, Force_y, 0);
            rigidBody.AddForce(Force);
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
            Initialize = false;
            ReleasedFlg = false;
            FireWindZone.SetActive(false);//WindZoneを非アクティブに
            FrameCount = 0;//フレームカウントを初期化
            ClickFlg = 99;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cylinder") {//地面に落ちたらスタートのポジションにリスポーン
            Force_y = 20.0f;//y軸に与える力を初期化
            FirstVelocity = true;//一度だけ入る処理をリセット
            ReleasedFlg = false;
            FrameCount = 0;//フレームカウントを初期化
            ClickFlg = 99;
            if (BoundFlg == true) {//階段での動き
                rigidBody.useGravity = true;

                rigidBody.velocity = _Vel;
                CircularMotion();//円運動
                BoundFlg = false;
            }
            count = AtanAngle;
        }
        if (collision.gameObject.tag == "LastWallCandle")
        {
            ClearInputFlg = false;
            GOAL.SetActive(true);

            MainCamera.enabled = false;
            ClearCamera.enabled = true;
        }
        if(collision.gameObject.tag == "Goal")
        {
            initialize = true;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Candle") {
            FireWindZone.SetActive(true);
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

    public void UpSpeedCandleCenterHit()//Speed変化
    {
        if (AddSpeedFlg) {
            //RotateSpeed += AddSpeed;
        } else if (!AddSpeedFlg) {
            RotateSpeed = tempRotateSpeed;
        }
        
    }

    private void MovementToClear()
    {
        ClearDirection = GOAL.transform.position - transform.position;//ベクトル取得
        ClearDirection.Normalize();//ベクトルを正規化

        rigidBody.useGravity = true;
        rigidBody.AddForce(ClearVelocity, ForceMode.Impulse);
    }

}

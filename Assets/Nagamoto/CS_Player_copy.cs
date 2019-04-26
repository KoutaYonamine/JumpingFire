using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player_copy : InitializeVariable     //サブクラス
{
    private Rigidbody rigidBody;

    private float x, y, z;//プレイヤーの移動座標

    private float FreeFallGrvity = 9.8f;//フレーム後に与える力
    private float UnnaturalGrvity = 19.6f;//指を離した時に与える力

    private Vector3 StartPosition;//初期位置
    private GameObject Camera;//カメラをゲットコンポーネント
    private Vector3 CameraPosition;//カメラのポジション

    private Stairscollision staircollision; //stairscollisionのスクリプト 変更点

    private float Length;//半径
    float AtanAngle;//方位角　角度
    float count;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        StartPosition = this.transform.position;
        Camera = GameObject.Find("Main Camera");
        CameraPosition = Camera.transform.position;

        Length = transform.position.magnitude - 0.5f;
        AtanAngle = Mathf.Atan2(StartPosition.x, StartPosition.z);
        count = AtanAngle;

        staircollision = GetComponent<Stairscollision>();
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
    }

    void InputMouse_Touch()
    {
        // エディタ、実機で処理を分ける
        if (Application.isEditor) {// エディタで実行中
            if (Input.GetMouseButtonDown(0) && staircollision.getmoveflag() == true && staircollision.getmouseflag() == true) {//押した時
                ClickFlg = 2;
                ReleasedFlg = true;
            }
            if (Input.GetMouseButton(0) && staircollision.getmoveflag() == true && staircollision.getmouseflag() == true) {//押し続けた時
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

                if (touch.phase == TouchPhase.Began && staircollision.getmoveflag() == true && staircollision.getmouseflag() == true)//押した瞬間
                {
                    ClickFlg = 2;
                    ReleasedFlg = true;
                }

                if (touch.phase == TouchPhase.Moved && staircollision.getmoveflag() == true && staircollision.getmouseflag() == true) {//押しっぱなし
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
            //transform.position = StartPosition;//初期位置にセット
            FirstVelocity = true;//一度だけ入る処理をリセット
            ReleasedFlg = false;
            FrameCount = 0;//フレームカウントを初期化
            ClickFlg = 99;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player_copy : InitializeVariable     //サブクラス
{
    private AudioSource audioSource; //サウンド
    private AudioClip JumpFireSounds;   //サウンド

    private Rigidbody rigidBody;

    private Vector3 ClearDirection;//クリアの聖火台の方向

    private float x, y, z;//プレイヤーの移動座標

    private float FreeFallGrvity = 9.8f;//フレーム後に与える力
    private float UnnaturalGrvity = 19.6f;//指を離した時に与える力

    [SerializeField] private float AddSpeed;//燭台の中心に乗った時にSpeedUp
    private float tempRotateSpeed;//RotateSpeedの退避用変数

    private GameObject GOAL;

    private Stairscollision staircollision; 

    private float Length;//半径
    private float AtanAngle;//方位角　角度
    private float count;
    Vector3 StairsVel　= new Vector3(3, 4, 3);//階段に落ちたときのバウンドVelocity

    public Camera MainCamera;
    public Camera ClearCamera;

    private bool DebugBoundFlg;
    private float BoundCount;

    private bool ClearClickCheck = true; //クリック制御

    /************燭台上でのバウンド処理*************/
    private float BoundGravity = 0.04f;//
    private float BoundForce;//
    private float TempBoundForce;
    private float TempBoundGravity;
    private int BoundCountUp = 0;
    private int countup = 0;
    private int TypeNumber;
    private bool check;
    private float ClickCount = 0;

    private Vector3 BoundFallPosition;
    private bool BoundFallFlg;

    private bool CheckGround;//Rayが地面に当たったかどうか

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();  //サウンド
        JumpFireSounds = audioSource.clip;  //サウンド

        rigidBody = GetComponent<Rigidbody>();

        FireWindZone.SetActive(false);//WindZoneを非アクティブに

        StartPosition = this.transform.position;

        Length = transform.position.magnitude - 0.5f;
        AtanAngle = Mathf.Atan2(StartPosition.x, StartPosition.z);
        count = AtanAngle;

        tempRotateSpeed = RotateSpeed;//RotateSpeedの値を退避
        //TempBoundForce = BoundForce;
        TempBoundGravity = BoundGravity;

        staircollision = GetComponent<Stairscollision>();

        GOAL = GameObject.Find("PublishFire_Prefab (1)");
        //GOAL.SetActive(false);

        ClearCamera.enabled = false;//クリア時のカメラを無効

    }

    // Update is called once per frame
    void Update()
    {
        InputMouse_Touch();
        
        if (ClickFlg == 2) {
            FrameCount++;
        }
        if(ClickFlg == 2 || ClickFlg == 0)
            ClickCount++;

        if (ClickFlg == 99 && rigidBody.useGravity == false) {
            rigidBody.velocity = Vector3.zero;
        }

        if (BoundFallFlg && this.transform.position.y < BoundFallPosition.y) {//直前に乗った燭台より下に落ちた(燭台から落下）
            staircollision.getmoveflag = false;
           
            //Rayの作成        原点                     方向
            Ray ray = new Ray(this.transform.position, Vector3.down);
            RaycastHit hit;//Rayが当たったオブジェクト情報を格納
            float RayDistance = 2.0f;//Rayの距離0.5
            //Rayの可視化  
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * RayDistance, Color.red);

            //Rayがオブジェクトに当たった時
            if (Physics.Raycast(ray, out hit, RayDistance)) {
                if (hit.collider.tag == "Cylinder") {
                    Force_y = 20.0f;//y軸に与える力を初期化
                    FirstVelocity = true;//一度だけ入る処理をリセット
                    ReleasedFlg = false;
                    FrameCount = 0;//フレームカウントを初期化
                    ClickFlg = 99;
                    BoundCount = count;
                    count = AtanAngle;

                    CheckGround = true;
                    GroundBound();
                }

                if(hit.collider.tag == "Goal")
                    BoundFallFlg = false;
            }
        }

        if (IsBound)
            BoundMotion();
        //Rayの作成        原点                     方向
        //Ray ray = new Ray(this.transform.position, Vector3.down);
        //RaycastHit hit;//Rayが当たったオブジェクト情報を格納
        //float RayDistance = 0.5f;//Rayの距離0.5
        //                         //Rayの可視化  
        //Debug.DrawLine(ray.origin, ray.origin + ray.direction * RayDistance, Color.red);

    }
    private void FixedUpdate()
    {
        FireMovement();
        
    }

    void InputMouse_Touch()
    {
        // エディタ、実機で処理を分ける
        if (Application.isEditor) {// エディタで実行中
            if (ClearInputFlg == true)
            {
                if (Input.GetMouseButtonDown(0) && staircollision.getmoveflag == true && staircollision.getmouseflag() == true && ClearInputFlg == true)
                {//押した時

                    ClickFlg = 2;
                    ReleasedFlg = true;
                    BoundFlg = true;                    
                }
                if (Input.GetMouseButtonUp(0) && ClearInputFlg == true)
                {//離した時
                    if (ReleasedFlg) {
                        ClickFlg = 0;
                        ReleasedFlg = false;
                        BoundFlg = true;
                    }
                }
            }
            if (Input.GetMouseButtonDown(0) && ClearInputFlg == false)//最後の燭台に乗った時ベクトルの方向変える
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

                if (touch.phase == TouchPhase.Began && staircollision.getmoveflag == true && staircollision.getmouseflag() == true && ClearInputFlg == true)//押した瞬間
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
                if (touch.phase == TouchPhase.Began && ClearInputFlg == false && ClearClickCheck) {
                    MovementToClear();
                    ClearClickCheck = false;
                }
            }
        }
    }

    void FireMovement()//Playerの挙動
    {
        if (ClickFlg == 0) {
            CircularMotion();//円運動
            
            Force_y = Force_y - UnnaturalGrvity;//離した時に急な落下をさせる

            Force = new Vector3(0, Force_y, 0);
            rigidBody.AddForce(Force);
        }

        if (ClickFlg == 2) {
            IsBound = false;//ジャンプしたらバウンド処理を無効
            JustOnce = false;
            BoundForce = TempBoundForce;
            BoundGravity = TempBoundGravity;
            BoundCountUp = 0;

            if (FirstVelocity) {//一度だけ入る
                audioSource.PlayOneShot(JumpFireSounds);    //サウンド
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
            Initialize = false;
            ReleasedFlg = false;
            FireWindZone.SetActive(false);//WindZoneを非アクティブに
            FrameCount = 0;//フレームカウントを初期化
            ClickFlg = 99;
            ClickCount = 0;
            if (JustOnce) {
                IsBound = true;
            }
            BoundCountUp++;
            BoundForce = TempBoundForce / 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cylinder") {//地面に落ちたらスタートのポジションにリスポーン
            
        }
        //if (collision.gameObject.tag == "LastWallCandle")//最後の燭台
        //{
        //    ClearInputFlg = false;
        //    GOAL.SetActive(true);

        //    MainCamera.enabled = false;
        //    ClearCamera.enabled = true;
        //}
        if (collision.transform.root.tag == "Candle") {//燭台に乗ったら
            JustOnce = true;
            
            /***乗った燭台の種類によってどんなバウンド処理をするかをCheck***/
            if (collision.transform.tag == "BlueCandle") {
                TypeNumber = 1;
                BoundForce = 0.1f;
                TempBoundForce = BoundForce;
            }
            if (collision.transform.tag == "BrownCandle") {
                TypeNumber = 2;
                BoundForce = 0.3f;
                TempBoundForce = BoundForce;
            }
            if (collision.transform.tag == "BlackCandle") {
                TypeNumber = 3;
            }

            BoundSeparate();//TypeNumberによってバウンド処理を分ける

        }

        if(collision.gameObject.tag == "Goal")
        {
        }
    }
   

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Candle") {

        }
    }

    private void OnCollisionExit(Collision collision)//コライダから抜けたとき
    {
        Vector3 CandlePos = collision.transform.position;
        if (collision.transform.root.tag == "Candle") {
            FireWindZone.SetActive(true);

            BoundFallPosition = collision.gameObject.transform.position;//最後に乗った燭台の座標を取得
            BoundFallFlg = true;
        }

        //if (collision.gameObject.tag == "Goal")
        //    staircollision.getmoveflag = true;
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
    public int clickflg
    {
        get { return ClickFlg; }
    }
    public float clickcount
    {
        get { return ClickCount; }
    }

    public int getBoundCountUp()
    {
        return BoundCountUp;
    }

    private void CircularMotion()//円運動
    {
        count += Time.deltaTime * RotateSpeed;

        x = Length * Mathf.Sin(count);
        y = transform.position.y;
        z = Length * Mathf.Cos(count);
        transform.position = new Vector3(x, y, z);
    }

    private void BoundMotion()//燭台の上でバウンド
    {
        CircularMotion();//円運動
        transform.Translate(Vector3.up * BoundForce);
        BoundForce = BoundForce - BoundGravity;      
    }
    private void BoundSeparate()
    {
        if (BoundCountUp == 1 && TypeNumber == 1) {
            //1回バウンド
            //音が2回なる
            //燭台の右側に着地するとそのまま落下
            JustOnce = false;
            IsBound = false;
            ClickFlg = 99;
        }
        if (BoundCountUp == 1 && TypeNumber == 2) {
            //2回バウンド
            //音が3回なる
            //燭台の左側に着地しないとそのまま落下
            JustOnce = false;
            IsBound = false;
            ClickFlg = 99;
        }
        if (TypeNumber == 3) {
            JustOnce = false;
            IsBound = false;
            rigidBody.isKinematic = true;
            rigidBody.isKinematic = false;
            rigidBody.velocity = Vector3.zero;
            //ClickFlg = 99;
        }
    }

    public void UpSpeedCandleCenterHit()//Speed変化
    {
        if (AddSpeedFlg) {
            //RotateSpeed += AddSpeed;
        } else if (!AddSpeedFlg) {
            RotateSpeed = tempRotateSpeed;
        }      
    }

    private void MovementToClear()//最後の燭台に乗った時ベクトルを変更
    {
        ClearDirection = GOAL.transform.position - transform.position;//ベクトル取得
        ClearDirection.Normalize();//ベクトルを正規化

        rigidBody.useGravity = true;
        rigidBody.AddForce(ClearVelocity, ForceMode.Impulse);
    }

    private void GroundBound()
    {
        if (BoundFlg && CheckGround) {//階段での動き
            rigidBody.useGravity = true;
            BoundFlg = false;
            CheckGround = false;

            IsBound = false;
            JustOnce = false;

            rigidBody.velocity = Vector3.zero;
            rigidBody.velocity = StairsVel;
            BoundFallFlg = false;

            BoundForce = TempBoundForce;
            BoundGravity = TempBoundGravity;
            BoundCountUp = 0;
        }
    }
}

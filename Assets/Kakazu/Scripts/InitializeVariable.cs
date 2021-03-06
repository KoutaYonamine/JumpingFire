﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeVariable : MonoBehaviour {//スーパークラス
    
    //プレイヤーに継承している変数
    static protected int ClickFlg = 99;//クリックしているかどうか
    static protected bool ReleasedFlg = false;//連打禁止 
    static protected bool FirstVelocity = true;//一度だけ入る(1フレーム目
    static protected bool AddSpeedFlg = false;//燭台の中心に当たったかどうか
    static protected bool Initialize = false; //スピード初期化判定
    static protected bool BoundFlg = false;//階段に落下した際の挙動判定
    static protected bool ClearInputFlg = true;//円運動の許可

    static protected int Candlestick = 0;                            //進んだ燭台の数
    static protected int numdigit = 0;                           //桁によるスコアの移動

    static protected bool Collision = false;                             //当たり判定用flag
    static protected bool Touchbool = false;                             //タッチアイコン用flag
    static protected bool softfocus = true;                             //ソフトフォーカスflag
    static protected bool Numflag = false;                               //ナンバーイメージflag
    static protected bool Staflag = true;                               //スタートflag
    static protected bool moveflag = false;                              //移動flag
    static protected bool mouseflag = true;                      //画面タッチflag
    static protected bool goalflag = false;                     //ゴールflag
    static protected bool StopBoneFire = false;

    static protected Number number;
    static protected GameObject Player;
    static protected GameObject numberobject;                    //ナンバーのオブジェクト
    protected GameObject FireWindZone;//WindZone
    protected ParticleSystem ParticleAlive;                 //炎のパーティクル

    static protected Vector3 StartPosition;                      //プレイヤーの最初の位置
    static protected Vector3 NumberPosition;                     //数字の位置
    static protected Vector3 NumberScale;                        //数字の大きさ

    static protected Rigidbody rd;

   /* [SerializeField]*/ static protected float Force_y = 20.0f;//yに与える力

    static protected Vector3 Force;//AddForce
    static protected float FrameCount = 0;//フレームをカウント

    static private GameObject camera;
    static private Vector3 CameraPosInit;       //カメラの初期位置

    /*[SerializeField]*/ static protected float RotateSpeed = 0.4f;//円運動の速度

    static protected Vector3 Vel = new Vector3(0, 5.0f, 0);//初速度
    static protected Vector3 ClearVelocity = new Vector3(0, 10.0f, -10.0f);//クリアの聖火台にジャンプする時のVelocity

    static protected bool IsBound = false;//燭台の上でバウンドするか
    static protected bool JustOnce = false;//

    //燭台に継承している変数

    //火のパーティクルのOn Off
    private void Awake()
    {
        Player = GameObject.Find("Fire");//Player
        numberobject = GameObject.Find("Number");       //ナンバーのオブジェクト取得
        StartPosition = GameObject.Find("Fire").transform.position;
        NumberPosition = GameObject.Find("Number").transform.position;  //数字の初期位置
        NumberScale = GameObject.Find("Number").transform.localScale;   //数字の初期大きさ
        FireWindZone = GameObject.Find("WindZoneManager");
        ParticleAlive = GameObject.Find("fire1_add").GetComponent<ParticleSystem>();

        camera = GameObject.FindGameObjectWithTag("MainCamera");       //カメラにアクセス
        CameraPosInit = new Vector3(camera.transform.position.x, Player.transform.position.y + 6.0f, camera.transform.position.z);      //カメラの初期位置確保
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    protected void ReloadInitializeVariable()//ゲームクリア、オーバー時に各変数を初期化
    {
        //プレイヤーに継承している変数
        ClickFlg = 99;//クリックしているかどうか
        ReleasedFlg = false;//連打禁止 
        FirstVelocity = true;//一度だけ入る(1フレーム目
        AddSpeedFlg = false;//燭台の中心に当たったかどうか
        Initialize = false; //スピード初期化判定
        ClearInputFlg = true;//円運動の許可

        Force_y = 20.0f;//yに与える力
        RotateSpeed = 0.4f;//円運動の速度

        ClearVelocity = new Vector3(0, 10.0f, -10.0f);//クリアの聖火台にジャンプする時のVelocity


        //Player.transform.position = StartPosition;    //スタート位置に行く
        Candlestick = 0;                                //進んだ燭台の数
        numdigit = 0;                                   //桁によるスコアの移動
        Staflag = false;                                 //スタートflag
        Collision = false;                              //当たっていないとき
        Touchbool = false;                              //タッチアイコンを表示しない
        softfocus = true;                               //ソフトフォーカス用flag
        Numflag = false;                                //ナンバーイメージflag
        moveflag = true;                                //移動flag
        mouseflag = false;
        goalflag = false;
        number.View(Candlestick);                       //最初の数字を読み込む
        rd.useGravity = false;
        rd.velocity = Vector3.zero;
        numberobject.transform.position = NumberPosition;
        numberobject.transform.localScale = NumberScale;
        StopBoneFire = false;

        IsBound = false;
        JustOnce = false;

        //Force;//AddForce
        FrameCount = 0;//フレームをカウント

        Vel = new Vector3(0, 5.0f, 0);//初速度

        camera.transform.position = CameraPosInit;      //カメラの位置を初期化
    }
}

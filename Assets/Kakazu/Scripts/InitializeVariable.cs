using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeVariable : MonoBehaviour {//スーパークラス

    //プレイヤーに継承している変数
    protected float ClickFlg = 99;//クリックしているかどうか
    protected bool ReleasedFlg = false;//連打禁止 
    protected bool FirstVelocity = true;//一度だけ入る(1フレーム目
    protected bool AddSpeedFlg = false;//燭台の中心に当たったかどうか
    protected bool Initialize = false; //スピード初期化判定
    protected bool BoundFlg = false;//階段に落下した際の挙動判定

    protected int Candlestick = 0;                            //進んだ燭台の数

    protected bool Collision = false;                             //当たり判定用flag
    protected bool Touchbool = false;                             //タッチアイコン用flag
    protected bool softfocus = true;                             //ソフトフォーカスflag
    protected bool Numflag = false;                               //ナンバーイメージflag
    protected bool Staflag = false;                               //スタートflag
    protected bool moveflag = true;                              //移動flag
    protected bool mouseflag = true;                      //画面タッチflag

    protected Number number;
    protected GameObject Player;
    protected GameObject numberobject;                    //ナンバーのオブジェクト

    protected Vector3 StartPosition;                      //プレイヤーの最初の位置
    protected Vector3 NumberPosition;                     //数字の位置
    protected Vector3 NumberScale;                        //数字の大きさ

    protected Rigidbody rd;

    protected float Force_y = 20.0f;//yに与える力

    protected Vector3 Force;//AddForce
    protected int FrameCount = 0;//フレームをカウント

    /*[SerializeField] */protected float RotateSpeed = 0.4f;//円運動の速度

    protected Vector3 Vel = new Vector3(0, 10.0f, 10);//初速度

    //燭台に継承している変数
    //火のパーティクルのOn Off

    // Use this for initialization
    void Start() {
        Player = GameObject.Find("Fire");
        numberobject = GameObject.Find("Number");       //ナンバーのオブジェクト取得
        StartPosition = GameObject.Find("Fire").transform.position;
        NumberPosition = GameObject.Find("Number").transform.position;  //数字の初期位置
        NumberScale = GameObject.Find("Number").transform.localScale;   //数字の初期大きさ

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

        Force_y = 20.0f;//yに与える力
        RotateSpeed = 0.4f;//円運動の速度

        //Player.transform.position = StartPosition;    //スタート位置に行く
        Candlestick = 0;                                //進んだ燭台の数
        Staflag = false;                                 //スタートflag
        Collision = false;                              //当たっていないとき
        Touchbool = false;                              //タッチアイコンを表示しない
        softfocus = true;                               //ソフトフォーカス用flag
        Numflag = false;                                //ナンバーイメージflag
        moveflag = true;                                //移動flag
        mouseflag = false;
        number.View(Candlestick);                       //最初の数字を読み込む
        rd.useGravity = false;
        rd.velocity = Vector3.zero;
        numberobject.transform.position = NumberPosition;
        numberobject.transform.localScale = NumberScale;

        //Force;//AddForce
        FrameCount = 0;//フレームをカウント

        Vel = new Vector3(0, 10.0f, 10);//初速度

    }
}

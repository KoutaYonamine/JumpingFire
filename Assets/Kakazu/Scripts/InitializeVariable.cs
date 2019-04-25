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

    protected float Force_y = 20.0f;//yに与える力

    protected Vector3 Force;//AddForce
    protected int FrameCount = 0;//フレームをカウント

    [SerializeField] protected float RotateSpeed;//円運動の速度
    protected Vector3 Vel = new Vector3(0, 10.0f, 10);//初速度

    //燭台に継承している変数
    //火のパーティクルのOn Off

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void ReloadInitializeVariable()//ゲームクリア、オーバー時に各変数を初期化
    {
    //プレイヤーに継承している変数
    ClickFlg = 99;//クリックしているかどうか
    ReleasedFlg = false;//連打禁止 
    FirstVelocity = true;//一度だけ入る(1フレーム目
    AddSpeedFlg = false;//燭台の中心に当たったかどうか
    Initialize = false; //スピード初期化判定

    Force_y = 20.0f;//yに与える力

    //Force;//AddForce
    FrameCount = 0;//フレームをカウント

    //RotateSpeed;//円運動の速度
    Vel = new Vector3(0, 10.0f, 10);//初速度

    }
}

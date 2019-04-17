using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stairscollision : MonoBehaviour {

    private int Candlestick;                            //進んだ燭台の数
    private float time;                                 //時間計測用
    private bool Collision;                             //当たり判定用Bool
    private bool Touchbool;                             //タッチアイコン用Bool
    private bool focusflag;                             //ソフトフォーカス用フラグ
    private GameObject numberobject;                    //ナンバーのオブジェクト
    private GameObject touchobject;                     //タッチアイコンのオブジェクト
    private Number number;                              //ナンバースクリプト
    
    // Use this for initialization
    void Start () {
        Candlestick = 0;                                //進んだ燭台の数
        time = 0;                                       //計測用
        Collision = false;                              //当たっていないとき
        Touchbool = false;                              //タッチアイコンを表示しない
        focusflag = false;                              //フォーカスしない
        numberobject = GameObject.Find("Number");       //ナンバーのオブジェクト取得
        touchobject = GameObject.Find("Touch");         //タッチアイコンのオブジェクト取得
        number = GameObject.Find("Canvas").GetComponent<Number>();  //ナンバースクリプトの取得
        
    }

    //Collisionflagを返す
    public bool getCollisionflag(){
        return Collision;
    }
    //focusflagを返す
    public bool getFocusflag(){
        return focusflag;
    }
    //Candlestickを返す
    public int getCandlestick(){
        return Candlestick;
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name == "Stairs"){      //触れたものが階段の場合
            Collision = true;
        }
        if(collision.gameObject.name == "Candlestick"){ //触れたものが燭台の場合
            Candlestick += 10;
            number.View(Candlestick);
        }
        
    }

    // Update is called once per frame
    void Update () {
        //タッチアイコンの表示/非表示
        if(Touchbool == false){
            touchobject.SetActive(false);       //タッチアイコンの非表示
        }
        else{
            touchobject.SetActive(true);        //タッチアイコンの表示
            if (Input.GetMouseButtonDown(0)){
                //初期化
                Candlestick = 0;                //進んだ燭台の数
                time = 0;                       //計測用
                Collision = false;              //当たっていないとき
                Touchbool = false;              //タッチアイコンを表示しない
                focusflag = false;              //フォーカスしない
            }
        }
        //触れてからの時間差
        if (Collision == true){  
            time += Time.deltaTime;             //時間計測
            if(time >= 2){                      //2秒以上たったら
            //numberobject.transform.localScale = new Vector3(2, 2, 0);           //数字の大きさを変える
            //numberobject.transform.localPosition = new Vector3(0, 150, 0);      //数字の位置を変える
            Touchbool = true;                   //スイッチオン
            focusflag = true;                   //ソフトフォーカスさせる
            }
        }
    }
}

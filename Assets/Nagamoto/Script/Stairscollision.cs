﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stairscollision : InitializeVariable
{
    private float time;                                   //時間計測用

    private GameObject touchobject;                       //タッチアイコンのオブジェクト
    private GameObject startobject;                       //スタートのオブジェクト

    // Use this for initialization
    void Start () {
        time = 0;                                       //計測用

        numberobject = GameObject.Find("Number");       //ナンバーのオブジェクト取得
        touchobject = GameObject.Find("Touch");         //タッチアイコンのオブジェクト取得
        startobject = GameObject.Find("Start");         //スタートのオブジェクト取得

        number = GameObject.Find("Canvas").GetComponent<Number>();  //ナンバースクリプトの取得
        //colliderLength_Copy = GameObject.Find("WallCandleStickUnited_01").GetComponent<ColliderLength_copy>();

        NumberPosition = GameObject.Find("Number").transform.position;  //数字の初期位置
        NumberScale = GameObject.Find("Number").transform.localScale;   //数字の初期大きさ

        rd = this.GetComponent<Rigidbody>();

        StartPosition = this.transform.position;        //スタート位置の保存  
        number.View(Candlestick);                       //最初の数字を読み込む


    }

    //Collisionflagを返す
    public bool getCollisionflag(){
        return Collision;
    }
    //Candlestickを返す
    public int getCandlestick(){
        return Candlestick;
    }
    //softfocusを返す
    public bool getsoftfocus(){
        return softfocus;
    }
    //staflagを返す
    public bool getstaflag(){
        return Staflag;
    }
    //moveflagを返す
    public bool getmoveflag(){
        return moveflag;
    }
    //mouseflagを返す
    public bool getmouseflag(){
        return mouseflag;
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name == "Stairs" || collision.gameObject.tag == "Cylinder"){      //触れたものが階段の場合
            Collision = true;
            moveflag = false;
        }
        if(collision.gameObject.name == "WallCandleStickUnited_01(Clone)"){    //触れたものが燭台の場合
            Candlestick += 1;
            number.View(Candlestick);
        }
        
    }

    // Update is called once per frame
    void Update () {
        //スタートの表示/非表示
        if (Staflag == false){
            startobject.SetActive(true);        //スタートイメージの表示
        }
        else{

            startobject.SetActive(false);       //スタートイメージの非表示
            Numflag = true;
        }
        //タッチアイコンの表示/非表示
        if (Touchbool == false){
            touchobject.SetActive(false);       //タッチアイコンの非表示
            softfocus = false;                  //ソフトフォーカスする
        }
        else{
            touchobject.SetActive(true);        //タッチアイコンの表示
            //タッチアイコンが出たとき
            if (Input.GetMouseButtonDown(0)||Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
                //初期化
                startobject.SetActive(true);
                this.transform.position = StartPosition;    //スタート位置に行く
                numberobject.transform.position = NumberPosition;
                numberobject.transform.localScale = NumberScale;
                ReloadInitializeVariable();
            }
        }
        if(mouseflag == false){
            time += Time.deltaTime;
            if(time >= 2){
                mouseflag = true;
                time = 0;
            }
        }
        //スタートを押したとき
        if (Input.GetMouseButtonDown(0) && mouseflag == true || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && mouseflag == true){
            Staflag = true;
        }
        //ナンバーイメージの表示/非表示
        if (Numflag == false){
            numberobject.SetActive(false);      //ナンバーを非表示
        }
        else{
            numberobject.SetActive(true);       //ナンバーを表示
        }
        //触れてからの時間差
        if (Collision == true){  
            time += Time.deltaTime;             //時間計測
            if(time >= 2){                      //2秒以上たったら
                numberobject.transform.localScale = new Vector3(0.75f, 0.75f, 0);           //数字の大きさを変える
                numberobject.transform.localPosition = new Vector3(0, 95, 0);               //数字の位置を変える
                Touchbool = true;                   //スイッチオン
                time = 0;
            }
        }
    }
}

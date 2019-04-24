﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player_copy : MonoBehaviour {

    private Rigidbody rigidBody;

    private int ClickFlg = 99;//クリックしているかどうか
    private bool IsJump = false;//
    private bool FirstVelocity = true;//一度だけ入る(1フレーム目

    private bool AddSpeedFlg; //燭台の中心に当たったかどうか
    private bool InitializeSpeedFlg; //スピード初期化
    private float NotBarrageCount = 0;//連打禁止   燭台に乗ったら入力受付


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

    private Stairscollision staircollision; //stairscollisionのスクリプト 変更点

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

        staircollision = GetComponent<Stairscollision>();
    }
    
    // Update is called once per frame
    void Update()
    {
        InputMouse_Touch();
        
        if(ClickFlg == 2)
            FrameCount++;

        //Debug.Log(rigidBody.velocity);
    }
    private void FixedUpdate()
    {
        RotateFire();
    }

    void InputMouse_Touch()
    {
        // エディタ、実機で処理を分ける
        if (Application.isEditor) {// エディタで実行中
            if (Input.GetMouseButtonDown(0) && staircollision.getstaflag() == true && staircollision.getmoveflag() == true) {//押した時 変更点
                Debug.Log("1");
                NotBarrageCount++;//何回押したかカウント
            }
            if (Input.GetMouseButton(0) && staircollision.getstaflag() == true && staircollision.getmoveflag() == true) {//押し続けた時　変更点
                if(NotBarrageCount == 1)
                    ClickFlg = 2;
                Debug.Log("2");
            }
            if (Input.GetMouseButtonUp(0)) {//離した時
                if(ClickFlg != 99)
                    ClickFlg = 0;
            }
        }
        else
        {
            // タッチされているかチェック
            if (Input.touchCount > 0)
            {
                // タッチ情報の取得
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && staircollision.getstaflag() == true && staircollision.getmoveflag() == true) //変更点
                {
                    //ClickFlg = 2;
                    NotBarrageCount++;
                    if (NotBarrageCount == 1)
                        ClickFlg = 2;
                    Debug.Log("押した瞬間");
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (ClickFlg != 99)
                        ClickFlg = 0;
                    Debug.Log("離した瞬間");
                }

                if (touch.phase == TouchPhase.Moved && staircollision.getstaflag() == true && staircollision.getmoveflag() == true)　//変更点
                {
                    if (NotBarrageCount == 1)
                        ClickFlg = 2;
                    Debug.Log("押しっぱなし");
                }
            }
        }
    }

    void RotateFire()
    {
        if (ClickFlg == 0) {

            count += Time.deltaTime * RotateSpeed;

            //円運動　
            x = Length * Mathf.Sin(count);
            y = transform.position.y;
            z = Length * Mathf.Cos(count);
            transform.position = new Vector3(x, y, z);

            Force_y = Force_y - UnnaturalGrvity;

            Force = new Vector3(0, Force_y, 0);
            rigidBody.AddForce(Force);
            FrameCount = 0;
        }

        if (ClickFlg == 2) {
            count += Time.deltaTime * RotateSpeed;
            if (FirstVelocity) {//一度だけ入る
                rigidBody.velocity = Vel;
                FirstVelocity = false;
            }
            if (FrameCount > 20) {//30フレーム超えたらForceに重力を加算
                Force_y = Force_y - FreeFallGrvity;
            }
            //円運動　
            x = Length * Mathf.Sin(count);
            y = transform.position.y;
            z = Length * Mathf.Cos(count);
            transform.position = new Vector3(x, y, z);

            Force = new Vector3(0, Force_y, 0);
            rigidBody.AddForce(Force);
        }

        if (ClickFlg == 0 && InitializeSpeedFlg == true)
        {
            Force_y = 20.0f;
            ClickFlg = 99;
            FirstVelocity = true;
            //rigidBody.isKinematic = true;
            rigidBody.isKinematic = false;
            rigidBody.velocity = Vector3.zero;
        }
        if (ClickFlg == 99) {
            NotBarrageCount = 0;
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn"){
            ClickFlg = 99;
            rigidBody.velocity = Vector3.zero;
            Force_y = 20.0f;
            FirstVelocity = true;
            count = AtanAngle;
            //transform.position = StartPosition;
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
    public bool initializespeed//燭台に乗ったかどうか
    {
        get { return InitializeSpeedFlg; }
        set { InitializeSpeedFlg = value; }
    }
    public float notbarragecount//連打禁止
    {
        get { return NotBarrageCount; }
        set { NotBarrageCount = value; }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Fire_Rework : MonoBehaviour {

    private Rigidbody rigidBody;

    private int ClickFlg = 99;//クリックしているかどうか
    private bool IsJump = false;
    private bool FallFlg = false;

    private float Speed;
    private float Radius;
    private float YPosition;

    private float x, y, z;
    private float TimeCount;
    private float TimeOut;

    private Vector3 Force;
    private float JumpGrvity;
    private int FrameCount;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        Speed = 1.0f;
        Radius = 10.0f;
        YPosition = 20.0f;
        TimeOut = 0.75f;

        JumpGrvity = 0.98f;
        FrameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        InputMouse_Touch();
        FrameCount++;
        Debug.Log(ClickFlg);
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
                ClickFlg = 1;
            }
            if (Input.GetMouseButton(0)) {//押し続けた時
                ClickFlg = 2;

            }
            if (Input.GetMouseButtonUp(0)) {//離した時
                ClickFlg = 0;
            }
        }
    }

    void RotateFire()
    {
        if (ClickFlg == 0) {
            YPosition = 20.0f;
            rigidBody.velocity = Vector3.zero;

            IsJump = false;
            FallFlg = true;
            FrameCount = 0;
            ClickFlg = 99;
        }
        if(ClickFlg == 1) {
            rigidBody.velocity = new Vector3(0, 10.0f, 0);//初速度
        }
        if (ClickFlg == 2) {
            //TimeCount += Time.deltaTime;

            //円運動　
            x = Radius * Mathf.Sin(Time.time * Speed);
            y = transform.position.y;
            z = Radius * Mathf.Cos(Time.time * Speed);
            transform.position = new Vector3(x, y, z);

            if(FrameCount > 30) {//20フレーム超えたら
                IsJump = true;
            }

            //if (TimeOut < TimeCount) {
            //    ClickFlg = 4;
            //}
            if (IsJump) {
                YPosition = YPosition - JumpGrvity;
            }

            Force = new Vector3(0, YPosition, 0);
            rigidBody.AddForce(Force);
        }

        //if (FallFlg) {
        //    JumpGrvity = 1.98f;
        //    YPosition = YPosition - JumpGrvity;
        //}
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLength_copy : InitializeVariable
{
    AudioSource[] audiosource; //サウンド
    AudioClip HitSounds; //サウンド
    AudioClip NotSounds; //サウンド

    private Vector3 P_Position; //プレイヤーのポジション
    private Vector3 Difference; //プレイヤーと燭台の差分

    private GameObject PlayerObj; //プレイヤーオブジェクト格納

    private CS_Player_copy CsPlayer; //CS_Playerをゲットコンポーネント
    private Rigidbody RigidPlayer;//Rigidbodyをゲットコンポーネント

    /*[SerializeField] */private float Length = 2.0f; //範囲チェック(inspectorで変更可能)
    private float Magnitude; //プレイヤーと燭台の距離

    private GameObject targetObject; //カメラを格納

    [SerializeField] float DifferenceY;

    private ParticleSystem BoneFire;

    // Use this for initialization
    void Start () {
        audiosource = GetComponents<AudioSource>();  //サウンド
        HitSounds = audiosource[0].clip;    //サウンド
        Debug.Log(HitSounds);
        NotSounds = audiosource[1].clip;    //サウンド
        Debug.Log(NotSounds);

        //BoneFire.SetActive(false);
        BoneFire = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        BoneFire.Stop();

        targetObject = GameObject.Find("Main Camera");
        this.transform.LookAt(new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z)); //燭台をカメラに向ける

        PlayerObj = GameObject.Find("Fire"); //プレイヤーを格納
        CsPlayer = PlayerObj.GetComponent<CS_Player_copy>();
        RigidPlayer = PlayerObj.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update () {
        if (StopBoneFire)
            BoneFire.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") //プレイヤーが触れたら
        {
            BoneFire.Play();//炎のParticleをアクティブに
            P_Position = collision.transform.position; //プレイヤーの座標を代入
            Difference = P_Position - transform.position; //差分
            Magnitude = Difference.magnitude;
            LengthCheck(); //フラグ切り替え
            PlayerObj.GetComponent<CS_Player_copy>().UpSpeedCandleCenterHit();
        }
        if (P_Position.y > transform.position.y + DifferenceY) {//燭台より上
            CsPlayer.initialize = true; //燭台に乗ったらtrue
            RigidPlayer.isKinematic = true;//物理挙動をカット
            RigidPlayer.useGravity = true;
            //RigidPlayer.velocity = Vector3.zero;
            //BoneFire.SetActive(false);
        }
        var mat = this.GetComponent<BoxCollider>().material;//使われているマテリアル取得

    }
    private void OnCollisionExit(Collision collision)
    {
        //CsPlayer.initialize = false;
    }

    private void LengthCheck() //フラグ切り替え
    {
        if(Magnitude <= Length)
        {
            audiosource[0].PlayOneShot(HitSounds); //サウンド

            CsPlayer.addspeed = true;

            //Debug.Log(CsPlayer.addspeed);
        }
        else if(Magnitude > Length)
        {
            audiosource[1].PlayOneShot(NotSounds); //サウンド

            CsPlayer.addspeed = false;
            //Debug.Log(CsPlayer.addspeed);
        }
    }
}

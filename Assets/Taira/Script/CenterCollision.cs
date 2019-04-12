using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCollision : MonoBehaviour {

    private bool Trigger;   //当たり
    private bool Collision; //はずれ

    private GameObject FireFX;

    public bool trigger//Triggerの値をPlayerに参照
    {
        get { return this.Trigger; }
        //private set { this.Trigger = value; }
    }

    // Use this for initialization
    void Start()
    {
        Trigger = false;
        Collision = false;

        //FireFX = GameObject.Find("FireFx");
        //FireFX.SetActive(false);パーティクル生成
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Trigger);
    }

    //当たりを感知する (真ん中)
    void OnTriggerEnter(Collider other)//触れた瞬間
    {
        if(other.gameObject.tag == "Player") {
            Trigger = true;
            AddSpeed();
        }
        //FireActiv();
        //Debug.Log("Hit Trigger");
    }
    private void OnTriggerStay(Collider other)//触れ続けている間
    {
        if (other.gameObject.tag == "Player") {
        }
    }
    private void OnTriggerExit(Collider other)//抜けた瞬間
    {
        if (other.gameObject.tag == "Player") {
            Trigger = false;
            //Debug.Log("抜けた");
        }
    }

    //はずれを感知する
    void OnCollisionEnter(Collision collision)
    {
        Collision = true;
        //FireActiv();

        //当たりに入ってなかったらスピードを元に戻す
        if(Trigger != true)
        {
            ReturnSpeed();
        }
    }

    //加速処理
    void AddSpeed()
    {
        Debug.Log("UP");
    }

    //元に戻す処理
    void ReturnSpeed()
    {
        Debug.Log("Return");
    }

    //パーティクルを表示
    //void FireActiv()
    //{
    //    FireFX.SetActive(true);
    //}
}
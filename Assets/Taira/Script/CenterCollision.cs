using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCollision : MonoBehaviour {

    private bool Trigger;   //当たり
    private bool Collision; //はずれ

    private GameObject FireFX;

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

    }

    //当たりを感知する (真ん中)
    void OnTriggerEnter(Collider collision)
    {
        Trigger = true;
        AddSpeed();
        //FireActiv();
        //Debug.Log("Hit Trigger");
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
        //Debug.Log("Hit");
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
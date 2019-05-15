using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Softfocus : MonoBehaviour {

    //private GameObject completely;                      
    private Image completely;                   //マルイメージ
    private float alfa;                         //アルファ値
    private float speed;                        //フェードアウトする速度
    private float red, green, blue;             //色
    private Stairscollision stair;              //Stairscollisionスクリプトの取得

    // Use this for initialization
    void Start () {
        completely = GameObject.Find("Completely").GetComponent<Image>();
        red = completely.color.r;               //赤を取得
        green = completely.color.g;             //緑を取得
        blue = completely.color.b;              //青を取得
        speed = 0.01f;                          //速度設定   
        stair = GameObject.Find("Fire").GetComponent<Stairscollision>();
    }

    // Update is called once per frame
    void Update () {
        if(stair.getCollisionflag() == true){
            completely.color = new Color(red, green, blue, alfa);
            if(alfa <= 0.5f){
                alfa += speed;
            }
        }
        if(stair.getsoftfocus() == true){
            alfa = 0f;
            completely.color = new Color(red, green, blue, alfa);
        }

    }
}

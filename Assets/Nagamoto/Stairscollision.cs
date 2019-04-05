using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stairscollision : MonoBehaviour {

    private int Candlestick;                            //進んだ燭台の数
    private bool Collision;                             //当たり判定用Bool
    private GameObject numberobject;                    //ナンバーのオブジェクト
    private GameObject touchobject;                     //タッチアイコンのオブジェクト
    private bool Touchbool;                             //タッチアイコン用Bool
    private float time;                                 //時間計測用
    private Text text;                                  //テキスト
    private Camera camera;                              //カメラ

    // Use this for initialization
    void Start () {
        Candlestick = 0;                                //進んだ燭台の数
        Collision = false;                              //当たっていないとき
        text = GameObject.Find("Textnumber").GetComponent<Text>();
        numberobject = GameObject.Find("Textnumber");   //ナンバーのオブジェクト取得
        touchobject = GameObject.Find("Touch");         //タッチアイコンのオブジェクト取得
        Touchbool = false;                              //タッチアイコンを表示しない
        time = 0;                                       //計測用
	}

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name == "Stairs"){      //触れたものが階段の場合
            Collision = true;
        }
        if(collision.gameObject.name == "Candlestick"){ //触れたものが燭台の場合
            Candlestick += 1;
            Debug.Log("a");
        }
        
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(Candlestick);
        //タッチアイコンの表示/非表示
        if(Touchbool == false){
            touchobject.SetActive(false);       //タッチアイコンの非表示
        }
        else{
            touchobject.SetActive(true);        //タッチアイコンの表示
            if (Input.GetMouseButtonDown(0)){
                SceneManager.LoadScene("start");
            }
        }
        //触れてからの時間差
        if(Collision == true){
            time += Time.deltaTime;             //時間計測
            Debug.Log(time);
            if(time >= 2){                      //2秒以上たったら
            numberobject.transform.localScale = new Vector3(1, 1, 0);           //数字の大きさを変える
            numberobject.transform.localPosition = new Vector3(0, 150, 0);      //数字の位置を変える
            Touchbool = true;                   //スイッチオン
            }
        }
        //点数加算の表示
        text.text = Candlestick.ToString();

    }
}

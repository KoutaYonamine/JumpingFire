using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Fire : MonoBehaviour {

    private int ClickFlg = 0;//クリックしているかどうか
    private float Speed;
    private float Radius;
    private float YPosition;
    private float X, Y, Z;

    // Use this for initialization
    void Start () {
        Speed = 1.0f;
        Radius = 2.0f;
        YPosition = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        InputMouse_Touch();
        RotateFire();
        //Debug.Log(transform.position);
    }

    void InputMouse_Touch()
    {
        // エディタ、実機で処理を分ける
        if (Application.isEditor) {// エディタで実行中
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("クリックした瞬間");
            }
            if (Input.GetMouseButtonUp(0)) {
                Debug.Log("離した瞬間");
                ClickFlg = 0;
            }
            if (Input.GetMouseButton(0)) {
                Debug.Log("クリックしっぱなし");
                ClickFlg = 1;
            }
        } else {// 実機で実行中
            //タッチされているかどうか
            if (Input.touchCount > 0) {
                //タッチ情報の取得
                Touch touch = Input.GetTouch(0);//GetTouch(X)→Xは取得する指の数

                if(touch.phase == TouchPhase.Began) {
                    Debug.Log("押した瞬間");
                }
                if(touch.phase == TouchPhase.Ended) {
                    Debug.Log("離した瞬間");
                }
                if(touch.phase == TouchPhase.Moved) {
                    Debug.Log("押しっぱなし");
                }
            }
        }
    }

    void RotateFire()
    {
        //X = Radius * Mathf.Sin(Time.time * Speed * ClickFlg);
        //Y = YPosition;
        //Z = Radius * Mathf.Sin(Time.time * Speed * ClickFlg);

        //transform.position = new Vector3(X, Y, Z);
        if(ClickFlg == 1) {
            X = Radius * Mathf.Sin(Time.time * Speed);
            Y = YPosition;
            Z = Radius * Mathf.Cos(Time.time * Speed);
            transform.position = new Vector3(X, Y, Z);
        }
    }
}

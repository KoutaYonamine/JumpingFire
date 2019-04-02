using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Fire : MonoBehaviour {

    private int ClickFlg = 99;//クリックしているかどうか
    private float Speed;
    private float Radius;
    private float YPosition;
    private float x, y, z;

    //2次関数 y = a(x-p)+q
    [SerializeField] private float X, Y;
    [SerializeField] private float a;
    [SerializeField] private float p, q;
    [SerializeField] private float powerX;

    // Use this for initialization
    void Start () {
        Speed = 1.0f;
        Radius = 2.0f;
        YPosition = 0.5f;

        //a = 0.05f;
        //p = 37.0f;
        //q = 62.0f;
        //X = 0; Y = 0;
    }
	
	// Update is called once per frame
	void Update () {
        InputMouse_Touch();
        RotateFire();
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
                //ClickFlg = 1;
                ClickFlg = 2;
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
        if(ClickFlg == 0) {
        }
        if(ClickFlg == 1) {
            x = Radius * Mathf.Sin(Time.time * Speed);
            y = YPosition;
            z = Radius * Mathf.Cos(Time.time * Speed);
            transform.position = new Vector3(x, y, z);
        }
        if(ClickFlg == 2) {
            X += powerX * Time.deltaTime;
            Y = -a * Mathf.Pow((X - p), 2.0f) + q;
            transform.position = new Vector3(transform.position.x + X, transform.position.y + (Y * Time.deltaTime), transform.position.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Fire : MonoBehaviour {

    private Rigidbody rigidBody;

    private int ClickFlg = 99;//クリックしているかどうか
    [SerializeField] private float Speed;
    [SerializeField] private float Radius;
    [SerializeField] private float YPosition;
    private float x, y, z;
    private float TimeCount;
    private float TimeOut;

    private Vector3 Force;

    //2次関数 y = a(x-p)+q
    //[SerializeField] private float X, Y;
    //[SerializeField] private float a;
    //[SerializeField] private float p, q;
    //[SerializeField] private float powerX;

    // Use this for initialization
    void Start () {

        rigidBody = GetComponent<Rigidbody>();

        Speed = 1.0f;
        Radius = 10.0f;
        YPosition = 1.0f;
        TimeOut = 0.75f;

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
                rigidBody.velocity = new Vector3(0, 5.0f, 0);
            }
            if (Input.GetMouseButtonUp(0)) {
                ClickFlg = 0;
                rigidBody.velocity = Vector3.zero;
            }
            if (Input.GetMouseButton(0)) {
                ClickFlg = 3;
                TimeCount += Time.deltaTime;
            }
        } else {// 実機で実行中
            //タッチされているかどうか
            if (Input.touchCount > 0) {
                //タッチ情報の取得
                Touch touch = Input.GetTouch(0);//GetTouch(X)→Xは取得する指の数

                if(touch.phase == TouchPhase.Began) {                   
                }
                if(touch.phase == TouchPhase.Ended) {
                }
                if(touch.phase == TouchPhase.Moved) {
                }
            }
        }
    }

    void RotateFire()
    {
        if(ClickFlg == 0) {
            Force = new Vector3(0, -YPosition, 0);
        }
        //if(ClickFlg == 1) {//円運動
        //    x = Radius * Mathf.Sin(Time.time * Speed);
        //    y = YPosition;
        //    z = Radius * Mathf.Cos(Time.time * Speed);
        //    transform.position = new Vector3(x, y, z);
        //}
        //if(ClickFlg == 2) {//横に放物線移動
        //    X += powerX * (Time.deltaTime * 1.5f);
        //    Y = -a * Mathf.Pow((X - p), 2.0f) + q;

        //    transform.position = new Vector3(transform.position.x + X, transform.position.y + (Y * Time.deltaTime * 1.5f), transform.position.z);
        //}
        if(ClickFlg == 3) {//円運動　+　AddForce
            x = Radius * Mathf.Sin(Time.time * Speed);
            y = transform.position.y;
            z = Radius * Mathf.Cos(Time.time * Speed);

            Force = new Vector3(0, YPosition, 0);
            rigidBody.AddForce(Force);
            transform.position = new Vector3(x, y, z);
            if(TimeOut < TimeCount) {
                ClickFlg = 0;
            }
        }
    }
}


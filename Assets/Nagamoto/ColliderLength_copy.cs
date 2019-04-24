using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLength_copy : MonoBehaviour {

    private Vector3 P_Position; //プレイヤーのポジション
    private Vector3 Difference; //プレイヤーと燭台の差分

    private bool LengthCheckFlg; //範囲内だとtrue;

    private GameObject PlayerObj; //プレイヤーオブジェクト格納

    private CS_Player_copy CsPlayer; //CS_Playerをゲットコンポーネント
    private Rigidbody RigidPlayer;//Rigidbodyをゲットコンポーネント

    [SerializeField] private float Length; //範囲チェック(inspectorで変更可能)
    private float Magnitude; //プレイヤーと燭台の距離

    private GameObject targetObject; //カメラを格納

    [SerializeField] float DifferenceY;

    // Use this for initialization
    void Start () {
        targetObject = GameObject.Find("Main Camera");
        this.transform.LookAt(new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z)); //燭台をカメラに向ける

        PlayerObj = GameObject.Find("Fire"); //プレイヤーを格納
        CsPlayer = PlayerObj.GetComponent<CS_Player_copy>();
        RigidPlayer = PlayerObj.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Player.initializespeed = true; //燭台に乗ったらtrue

        if (collision.gameObject.tag == "Player") //プレイヤータグ？
        {
            P_Position = collision.transform.position; //プレイヤーの座標を代入
            Difference = P_Position - transform.position; //差分
            Magnitude = Difference.magnitude;
            LengthCheck(); //フラグ切り替え
        }
        if (P_Position.y > transform.position.y + DifferenceY) {
            CsPlayer.initialize = true; //燭台に乗ったらtrue
            RigidPlayer.isKinematic = true;//物理挙動をカット
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        CsPlayer.initialize = false;
    }

    private void LengthCheck() //フラグ切り替え
    {
        if(Magnitude <= Length)
        {
            CsPlayer.addspeed = true;
            //Debug.Log(Player.addspeed);

            LengthCheckFlg = true; //当たり
            //Debug.Log("当たり");
        }
        else if(Magnitude > Length)
        {
            LengthCheckFlg = false; //はずれ
            //Debug.Log("はずれ");
        }
    }
}

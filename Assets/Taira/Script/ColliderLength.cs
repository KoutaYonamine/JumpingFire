using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLength : MonoBehaviour {

    private Vector3 P_Position; //プレイヤーのポジション
    private Vector3 Difference; //プレイヤーと燭台の差分

    private bool LengthCheckFlg; //範囲内だとtrue;

    public GameObject PlayerObj; //プレイヤーオブジェクト格納
    private CS_Player Player; //プレイヤーゲットコンポーネント

    [SerializeField] private float Length; //範囲チェック(inspectorで変更可能)
    private float Magnitude; //プレイヤーと燭台の距離

    // Use this for initialization
    void Start () {
        Player = PlayerObj.GetComponent<CS_Player>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") //プレイヤータグ？
        {
            P_Position = collision.transform.position; //プレイヤーの座標を代入
            Difference = P_Position - transform.position; //差分
            Magnitude = Difference.magnitude;
            Debug.Log(Magnitude);
            LengthCheck(); //フラグ切り替え
        }
    }

    private void LengthCheck() //フラグ切り替え
    {
        if(Magnitude <= Length)
        {
            Player.addspeed = true;
            Debug.Log(Player.addspeed);

            LengthCheckFlg = true; //当たり
            Debug.Log("当たり");
        }
        else if(Magnitude > Length)
        {
            LengthCheckFlg = false; //はずれ
            Debug.Log("はずれ");
        }
    }
}

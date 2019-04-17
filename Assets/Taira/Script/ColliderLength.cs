using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLength : MonoBehaviour {

    private Vector3 P_Position;
    private Vector3 Difference;
    private float Magnitude;
    private bool LengthCheckFlg;

    [SerializeField] private float Length;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("あ");
            P_Position = collision.transform.position; //プレイヤーの座標を代入
            Difference = P_Position - transform.position;
            Magnitude = Difference.magnitude;
            Debug.Log(Magnitude);
            LengthCheck();
        }
    }

    private void LengthCheck()
    {
        if(Magnitude <= Length)
        {
            Debug.Log("当たり");
        }
        else if(Magnitude > Length)
        {
            Debug.Log("はずれ");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // 位置座標
    private Vector3 PlayerPos;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MovementPlayer();
        InputPosition();
    }

    void MovementPlayer()
    {
        transform.position += transform.up * Time.deltaTime;
    }

    void InputPosition()
    {
        if (Input.GetMouseButton(0))
        {
            MousePosToPlayer();
        }
    }

    void MousePosToPlayer()
    {
        // Vector3でマウス位置座標を取得する
        PlayerPos = Input.mousePosition;

        // Z軸修正
        PlayerPos.z = 10f;

        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(PlayerPos);

        // ワールド座標に変換されたマウス座標を代入
        gameObject.transform.position = screenToWorldPointPosition;
    }
}

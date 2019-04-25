using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField] private Transform pivot;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(pivot);    //プレイヤーを見る

        //transform.position = new Vector3(transform.position.x, pivot.position.y, transform.position.z);
	}
    private void LateUpdate()
    {
        
        transform.position = new Vector3(transform.position.x , pivot.position.y +6, transform.position.z);
        //transform.LookAt(new Vector3(pivot.position.x, pivot.position.y, pivot.position.z));
        transform.LookAt(pivot.position + transform.right * 3 + transform.up * -2);
    }
}

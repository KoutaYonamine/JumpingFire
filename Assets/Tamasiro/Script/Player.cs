using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //[SerializeField] private Transform pivot;
    [SerializeField] private float radius;
    [SerializeField] private float speed;

    float xc;
    float yc;
    float zc;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //回転
        xc = radius * Mathf.Sin(Time.time * speed);
        yc = Time.time * speed;
        zc = radius * Mathf.Cos(Time.time * speed);

        transform.position= new Vector3(xc, yc, zc);

        //クリックしたとき
        //if (Input.GetMouseButton(0))
        //{
        //    transform.position = new Vector3(xc, yc, zc);
        //}
    }
}

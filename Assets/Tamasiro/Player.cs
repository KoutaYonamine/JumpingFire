using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private Transform pivot;
    //[SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private float elevation;
    [SerializeField] private float azimuth;

    private const float PI = 3.14f;
    private const float ANGLE = 180.0f;

    float xc;
    float yc;
    float zc;

    float hangle = 0.0f;
    float vangle = 45.0f;
    float radius = 5.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float px = Input.GetAxis("Horizontal");
        float py = Input.GetAxis("Vertical");

        //xc = radius * Mathf.Cos(vangle * PI / ANGLE) * Mathf.Cos(hangle * PI / ANGLE);
        //yc = radius * Mathf.Sin(vangle * PI / ANGLE);
        //zc = radius * Mathf.Cos(vangle * PI / ANGLE) * Mathf.Sin(hangle * PI / ANGLE);

        xc = radius * Mathf.Sin(Time.time * speed);
        yc = 0;
        zc = radius * Mathf.Cos(Time.time * speed);

        transform.position= new Vector3(xc, yc, zc);

        float y = 0.1f;

        if(Input.GetMouseButton(0)) {

            transform.Translate(0, y, 0.0f);

        }        
	}
}

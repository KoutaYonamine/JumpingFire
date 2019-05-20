using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour {

    private Rigidbody rbody;
    private float speed;
    private float radius;
    private float yPosition;
    private Vector3 defaultPos;

    private float ClickFlg = 99;

    private float Length;
    private float count;
    private float AtanAngle;
    private float RotateSpeed = 0.4f;

    private Vector3 StartPosition;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();

        StartPosition = this.transform.position;
        speed = 1.0f;
        radius = 2.0f;

        Length = transform.position.magnitude;
        AtanAngle = Mathf.Atan2(StartPosition.x, StartPosition.z);
        count = AtanAngle;
    }

    // Update is called once per frame
    void Update()
    {
        //RotateMovement();
        //CircularMotion();
        Input_Mouse();
        PlayerMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //transform.position = Vector3.Lerp(transform.position, new Vector3(StartPosition.x, StartPosition.y + 20.0f, StartPosition.z), Time.deltaTime);
        transform.Translate(Vector3.up * 1.0f, Space.Self);
    }

    private void Input_Mouse()
    {
        if (Input.GetMouseButtonDown(0)){
            ClickFlg = 1;
            transform.position = Vector3.Lerp(transform.position, new Vector3(StartPosition.x, StartPosition.y + 5.0f, StartPosition.z), Time.deltaTime * 2);
        }
        if (Input.GetMouseButtonUp(0)) {
            ClickFlg = 0;
            //transform.Translate(Vector3.up * 1.0f);
        }
        if (Input.GetMouseButton(0)) {
            //transform.Translate(Vector3.up * 0.05f);
        }
    }

    private void PlayerMovement()
    {
        if(ClickFlg == 1) {
            //RotateMovement();
            CircularMotion();
            //float Force_y = 10;
            //Vector3 Force = new Vector3(1, Force_y, 1);//y座標に力を加算
            //rbody.AddForce(Force);
            //Debug.Log(rbody.velocity);
        }
    }

    private void RotateMovement()
    {
        count += Time.deltaTime * RotateSpeed;

        rbody.MovePosition(
           new Vector3(
               radius * Mathf.Sin(count *5),
               transform.position.y,
               radius * Mathf.Cos(count *5)
           )
       );
    }
    private void CircularMotion()//円運動
    {
        count += Time.deltaTime * RotateSpeed;

        float x = Length * Mathf.Sin(count);
        float y = transform.position.y;
        float z = Length * Mathf.Cos(count);
        transform.position = new Vector3(x, y, z);
    }
}

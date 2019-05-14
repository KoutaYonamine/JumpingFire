using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private GameObject _Cube;

    // Use this for initialization
    void Start()
    {
        _Cube =  GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            _Cube.transform.Translate(Vector3.right * 1.5f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            _Cube.transform.Translate(Vector3.right * -1.5f * Time.deltaTime);
        }
    }
}

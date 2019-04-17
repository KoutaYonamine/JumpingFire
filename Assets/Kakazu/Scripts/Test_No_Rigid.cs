using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_No_Rigid : MonoBehaviour {

    float v;
    [SerializeField]float v0 = 20;
    [SerializeField] float a = -9.8f;
    [SerializeField] float t = 0;
    float _y;

    void Start()
    {
       
    }

    void Update()
    {
        t += Time.deltaTime;

        v = v0 + (a * t);
        _y = (v0 * t) + ((a * Mathf.Pow(t, 2)) / 2);
        Debug.Log("v :"+v + "_y :"+_y + "t :"+t);

        transform.position = new Vector3(0, _y, 0);
    }
}

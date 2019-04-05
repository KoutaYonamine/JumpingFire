using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManeger : MonoBehaviour {

    BoxCollider[] a;

    // Use this for initialization
    void Start()
    {
        a = GetComponents<BoxCollider>();
        int i = 0;
        foreach (BoxCollider test in a)
        {
            a[i++] = test;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        //衝突判定
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Good!!");
        }
    }
}
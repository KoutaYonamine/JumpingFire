using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLength : MonoBehaviour {

    Vector3 pos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(pos = (GameObject.Find("Cube")).GetComponent<Renderer>().bounds.center);
	}
}

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
        transform.LookAt(pivot);

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {

    private bool ActiveTag = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody r = other.gameObject.GetComponent<Rigidbody>();

        Vector3 direction = transform.position - other.gameObject.transform.position;
        direction.Normalize();
        if (other.gameObject.tag == "Player") {
            ActiveTag = true;

            r.velocity *= 0.5f;
            r.AddForce(direction * r.mass * 20.0f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player") {
            ActiveTag = false;
        }
    }
}

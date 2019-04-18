using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMultiple : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Collider[] n = GetComponents<Collider>();
        int i = 0;
        foreach (Collider collider in n)
        {
            n[i++] = collider;
        }
        Debug.Log(n[1].isTrigger);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
    }
}

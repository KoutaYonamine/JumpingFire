using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearInstantiation : MonoBehaviour {

    public GameObject Nuclear;

	// Use this for initialization
	void Start() { 
	}
	
	// Update is called once per frame
	void Update () {
        OnClick();

    }

    void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Nuclear);
        }
    }
}

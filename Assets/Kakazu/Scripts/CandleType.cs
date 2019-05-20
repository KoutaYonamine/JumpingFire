using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleType : MonoBehaviour {

    private int TypeNumber = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TypeCheck()
    {
        if (transform.name == "WallCandleStickUnited_01 (2) 1 1(Clone)")
            TypeNumber = 0;
    }
}

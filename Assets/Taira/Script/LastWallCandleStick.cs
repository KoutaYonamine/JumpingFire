using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastWallCandleStick : MonoBehaviour {

    bool LastFlg = false;

	// Use this for initialization
	void Start () {
        Debug.Log(LastFlg);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        LastFlg = true;
        Debug.Log(LastFlg);
    }
}

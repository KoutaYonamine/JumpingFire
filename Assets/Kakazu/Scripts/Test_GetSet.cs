using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_GetSet : MonoBehaviour {

    private bool TestFlg = false;

    public bool Test
    {
        get
        {
            return TestFlg;
        }
        set
        {
            TestFlg = value;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(TestFlg);
	}
}

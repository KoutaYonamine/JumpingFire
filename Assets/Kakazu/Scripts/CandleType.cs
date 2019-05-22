using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleType : MonoBehaviour {

    private int TypeNumber = 99;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
    }

    void TypeCheck()
    {
        if (transform.name == "WallCandleStickUnited_01 (2) 1 1(Clone)") {
            Debug.Log(TypeNumber);
            TypeNumber = 1;
        }
        if (transform.name == "name1")
            TypeNumber = 2;
        if (transform.name == "name2")
            TypeNumber = 3;
    }

    //public int typenumber//playerにどの燭台に乗ったかを伝える
    //{
    //    get { return TypeNumber; }
    //    set { TypeNumber = value; }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player") {
    //        TypeCheck();
    //        //TypeNumber++;
    //        Debug.Log(TypeNumber + " : Candle");
    //    }
    //}
}

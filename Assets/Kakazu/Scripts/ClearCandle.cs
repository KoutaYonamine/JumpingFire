using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCandle : MonoBehaviour {

    public GameObject Player;
    private CS_Player_copy PlayerScript;
    private Rigidbody RigidPlayer;

	// Use this for initialization
	void Start () {
        PlayerScript = Player.GetComponent<CS_Player_copy>();
        RigidPlayer = Player.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") {
            PlayerScript.initialize = true;
            RigidPlayer.isKinematic = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCandle : MonoBehaviour {

    private GameObject ClearBoneFire;

    AudioSource audioSource;
    AudioClip audioClip;

    public GameObject Player;
    private CS_Player_copy PlayerScript;
    private Rigidbody RigidPlayer;

	// Use this for initialization
	void Start () {
        PlayerScript = Player.GetComponent<CS_Player_copy>();
        RigidPlayer = Player.GetComponent<Rigidbody>();
        ClearBoneFire = GameObject.Find("ClearCampFire");
        ClearBoneFire.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.clip;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") {
            ClearBoneFire.SetActive(true);
            audioSource.PlayOneShot(audioClip);

            PlayerScript.initialize = true;
            RigidPlayer.isKinematic = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //audioSource.Stop();

        //ClearBoneFire.SetActive(false);
        //this.gameObject.SetActive(false);
    }
}

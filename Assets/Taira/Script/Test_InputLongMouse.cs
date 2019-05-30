using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_InputLongMouse : MonoBehaviour {

    AudioSource audioSource;
    AudioClip audioClip;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioClip = audioSource.clip;
	}
	
	// Update is called once per frame
	void Update () {
        MouseClick();
	}

    void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(audioClip);
        }else if (Input.GetMouseButtonUp(0))
        {
            audioSource.Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlaySounds : MonoBehaviour {

    private AudioSource audiosource;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        if (true /*startのフラグが切り替えられたら*/)
        {
            audiosource.time = 0.1f;
            audiosource.PlayOneShot(audiosource.clip);
        }
    }
}

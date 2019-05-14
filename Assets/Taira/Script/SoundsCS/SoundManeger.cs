using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManeger : MonoBehaviour {

    private AudioSource[] audioSources;
    private AudioClip[] audioClips;

	// Use this for initialization
	void Start () {
        audioSources = GetComponents<AudioSource>();  //サウンド
        audioClips = new AudioClip[audioSources.Length];
        for(int i = 0; i < audioSources.Length; i++)
        {
            audioClips[i] = audioSources[i].clip;
            //Debug.Log(audioClips[i]);
            Debug.Log(audioClips[0]);
            Debug.Log(audioClips[1]);

        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            PlaySe(1);
        }
	}

    void PlaySe(int Se)
    {
        audioSources[Se].PlayOneShot(audioClips[Se]);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
    [SerializeField]
    AudioSource initialSong;
    [SerializeField]
    AudioSource loopSong;
	void Start ()
    {
        Invoke("PlayLoop", initialSong.clip.length - 3);
		
	}

    void PlayLoop()
    {
        loopSong.Play();
    }
    
}

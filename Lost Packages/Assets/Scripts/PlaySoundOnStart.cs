﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spielt einen Sound ab
public class PlaySoundOnStart : MonoBehaviour
{
    AudioSource sound;
    public string source;

    void Start()
    {
        sound = GameObject.Find(source).GetComponent<AudioSource>();
        sound.Play(0);
    }
}

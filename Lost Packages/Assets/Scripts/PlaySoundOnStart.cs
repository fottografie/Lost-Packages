using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    AudioSource sound;
    public string source;

    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.Find(source).GetComponent<AudioSource>();
        sound.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

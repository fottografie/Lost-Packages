﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInsel : MonoBehaviour
{
    public string levelLink;
    AudioSource buttonHit;

    private void OnMouseDown()
    {
        buttonHit = GameObject.Find("AudioButtonHit").GetComponent<AudioSource>();
        buttonHit.Play(0);
<<<<<<< Updated upstream
        SceneManager.LoadScene(levelLink, LoadSceneMode.Single);
=======
        if (levelLink == "Menue")
        {
            SceneManager.LoadScene("Menue", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(levelLink, LoadSceneMode.Single);
        }
>>>>>>> Stashed changes
    }
}

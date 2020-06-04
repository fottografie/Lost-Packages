using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInsel : MonoBehaviour
{
    public string levelLink;
    AudioSource buttonHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        buttonHit = GameObject.Find("AudioButtonHit").GetComponent<AudioSource>();
        buttonHit.Play(0);
        SceneManager.LoadScene(levelLink, LoadSceneMode.Single);
    }
}

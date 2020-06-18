using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Öffnet eine neue Scene
public class OpenLevel : MonoBehaviour
{
    public string levelName;
    public bool wiederholen;

    AudioSource buttonHit;

    private void Start()
    {
        //PlayerPrefs.SetInt("Menue", 0);
    }

    //Für Objekte mit Collider
    private void OnMouseDown()
    {
        buttonHit = GameObject.Find("AudioButtonHit").GetComponent<AudioSource>();
        buttonHit.Play(0);

        if (levelName == "next" || wiederholen)
        {
            int nextLevel = PlayerPrefs.GetInt("NextScene");
            if (wiederholen)
            {
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Level0" + (nextLevel));
                //SceneManager.LoadScene("Level0" + (nextLevel), LoadSceneMode.Single);
            }
            else
            {
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Level0" + (nextLevel + 1));
                //SceneManager.LoadScene("Level0" + (nextLevel + 1), LoadSceneMode.Single);
            }
        }
        else
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel(levelName);
            //SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }
        
    }

    //Für Buttons
    public void LoadMenu()
    {
        buttonHit = GameObject.Find("AudioButtonHit").GetComponent<AudioSource>();
        buttonHit.Play(0);
        if (PlayerPrefs.GetInt("Menue") == 1)
        {
            PlayerPrefs.SetInt("Menue", 0);
            SceneManager.UnloadSceneAsync("Menue");
        }
        else
        {
            PlayerPrefs.SetInt("Menue", 1);
            SceneManager.LoadScene("Menue", LoadSceneMode.Additive);
        }
    }

    public void UnloadMenue()
    {
        buttonHit = GameObject.Find("AudioButtonHit").GetComponent<AudioSource>();
        buttonHit.Play(0);
        SceneManager.UnloadSceneAsync("Menue");
        PlayerPrefs.SetInt("Menue", 0);
    }

    public void LoadLevel()
    {
        buttonHit = GameObject.Find("AudioButtonHit").GetComponent<AudioSource>();
        buttonHit.Play(0);
        if (wiederholen)
        {
            int nextLevel = PlayerPrefs.GetInt("NextScene");
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Level0" + (nextLevel + 1));
            //SceneManager.LoadScene("Level0" + (nextLevel + 1), LoadSceneMode.Single);
        }
        else
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel(levelName);
            //SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }
    }
}

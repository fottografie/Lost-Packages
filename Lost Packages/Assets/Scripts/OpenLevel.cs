using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Öffnet eine neue Scene
public class OpenLevel : MonoBehaviour
{
    public string levelName;
    public bool wiederholen;

    public int minCoins;
    public Text minCoinLabel;

    AudioSource buttonHit;

    private void Start()
    {
        if (minCoinLabel != null) { 
            minCoinLabel.GetComponent<Text>().text = "" + minCoins;
        }

        //PlayerPrefs.SetInt("Menue", 0);
    }

    //Für Objekte mit Collider
    private void OnMouseDown()
    {
        if (levelName == "next" || wiederholen)
        {
            int nextLevel = PlayerPrefs.GetInt("NextScene");
            if (wiederholen)
            {
                buttonHit = GameObject.Find("AudioButtonHit").GetComponent<AudioSource>();
                buttonHit.Play(0);
                Debug.Log(PlayerPrefs.GetInt("aktuellesLevel"));
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Level0" + PlayerPrefs.GetInt("aktuellesLevel"));
            }
            else
            {
                buttonHit = GameObject.Find("AudioButtonHit").GetComponent<AudioSource>();
                buttonHit.Play(0);
                if(nextLevel + 1 >= 6)
                {
                    GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Start");
                }
                else
                {
                    GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Level0" + (nextLevel + 1));
                }
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Coins") >= minCoins)
            {
                buttonHit = GameObject.Find("AudioButtonHit").GetComponent<AudioSource>();
                buttonHit.Play(0);
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel(levelName);
            }
            else
            {
                GameObject.Find("DialogTrigger").GetComponent<DialogueTrigger>().TriggerDialogue();
            }
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
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Level0" + PlayerPrefs.GetInt("aktuellesLevel"));
        }
        else
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel(levelName);
        }
    }
}

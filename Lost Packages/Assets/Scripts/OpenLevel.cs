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

    private void Start()
    {
        if (minCoinLabel != null) { 
            minCoinLabel.GetComponent<Text>().text = "" + minCoins;
        }
    }

    //Für Objekte mit Collider
    private void OnMouseDown()
    {
        if (levelName == "next" || wiederholen)
        {
            int nextLevel = PlayerPrefs.GetInt("NextScene");
            if (wiederholen)
            {
                FindObjectOfType<AudioManager>().PlayRandomOfKind("ButtonHit2_", 7);
                Debug.Log(PlayerPrefs.GetInt("aktuellesLevel"));
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Level0" + PlayerPrefs.GetInt("aktuellesLevel"));
            }
            else
            {
                FindObjectOfType<AudioManager>().PlayRandomOfKind("ButtonHit2_", 7);
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
                FindObjectOfType<AudioManager>().PlayRandomOfKind("ButtonHit2_", 7);
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
        FindObjectOfType<AudioManager>().PlayRandomOfKind("ButtonHit2_", 7);
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
        FindObjectOfType<AudioManager>().PlayRandomOfKind("ButtonHit2_", 7);
        SceneManager.UnloadSceneAsync("Menue");
        PlayerPrefs.SetInt("Menue", 0);
    }

    public void LoadLevel()
    {
        FindObjectOfType<AudioManager>().PlayRandomOfKind("ButtonHit2_", 7);
        if (wiederholen)
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Level0" + PlayerPrefs.GetInt("aktuellesLevel"));
        }
        else if(levelName == "next")
        {
            int nextLevel = PlayerPrefs.GetInt("NextScene");
            if (nextLevel == 5)
            {
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Ende");
            }
            else
            {
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Level0" + (nextLevel + 1));
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Coins") >= minCoins)
            {
                FindObjectOfType<AudioManager>().PlayRandomOfKind("ButtonHit2_", 7);
                GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel(levelName);
            }
            else
            {
                GameObject.Find("DialogTrigger").GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }


    }
}

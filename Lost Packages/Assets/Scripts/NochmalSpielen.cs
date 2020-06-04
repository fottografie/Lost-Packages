using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NochmalSpielen : MonoBehaviour
{
    public int nextLevel;
    public bool wiederholen;

    private void OnMouseDown()
    {
        nextLevel = PlayerPrefs.GetInt("NextScene");
        if (!wiederholen)
        {
            SceneManager.LoadScene("Level0" + (nextLevel + 1), LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Level0" + (nextLevel), LoadSceneMode.Single);
        }
    }
}

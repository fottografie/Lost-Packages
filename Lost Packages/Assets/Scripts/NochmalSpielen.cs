using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NochmalSpielen : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}

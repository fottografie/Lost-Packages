using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controller der anzeigt, wie viele Züge man benötigt hat
public class WinController : MonoBehaviour
{
    public Text zuegeLabel;
    private int zuege;

    public Text coinsLabel;
    private int anzahl;

    void Start()
    {
        zuege = PlayerPrefs.GetInt("Zuganzahl");
        zuegeLabel.GetComponent<Text>().text = "Du hast " + (PlayerPrefs.GetInt("maxZuege") - zuege) + " Züge benötigt";

        anzahl = 5;
        GameObject.Find("AudioManager").GetComponent<AudioManager>().FadeOutController("BackgroundWaves");
        StartCoroutine(AddOne());
    }

    IEnumerator AddOne()
    {
        for (int i = 1; i < anzahl + 1; i++)
        {
            yield return new WaitForSeconds(0.17f + i/anzahl);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
            coinsLabel.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("Coins");
        }
    }
}

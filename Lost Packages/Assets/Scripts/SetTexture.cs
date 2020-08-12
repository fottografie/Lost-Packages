using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTexture : MonoBehaviour
{
    public string gegenstand;
    public int index;
    public int coins;
    public Text coinsLabel;

    public GameObject coinLabel;
    public GameObject strich;
    public GameObject haken;
    public bool paketBool;

    private void Start()
    {
        coinsLabel.GetComponent<Text>().text = "" + coins;

        if (PlayerPrefs.GetInt(gegenstand + index) == 1)
        {
            coinLabel.active = false;
            //strich.active = true;
        }
        else
        {
            //strich.active = false;
        }
        //PlayerPrefs.SetInt("Coins", 80);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("spielerTexture") == index && !paketBool)
        {
            haken.active = true;
        }
        else if(PlayerPrefs.GetInt("spielerTexture") != index && !paketBool)
        {
            haken.active = false;
        }

        if (PlayerPrefs.GetInt("paketTexture") == index && paketBool)
        {
            haken.active = true;
        }
        else if (PlayerPrefs.GetInt("paketTexture") != index && paketBool)
        {
            haken.active = false;
        }
    }



    public void SetTextures()
    {
        if (coins <= PlayerPrefs.GetInt("Coins") || PlayerPrefs.GetInt(gegenstand + index) == 1)
        {
            FindObjectOfType<AudioManager>().PlayRandomOfKind("ButtonHit2_", 7);
            PlayerPrefs.SetInt(gegenstand, index);
            //Debug.Log(PlayerPrefs.GetInt(gegenstand));
            if(PlayerPrefs.GetInt(gegenstand + index) == 0)
            {
                FindObjectOfType<AudioManager>().PlayRandomOfKind("CoinSound", 5);
                int newCoins = PlayerPrefs.GetInt("Coins") - coins;
                PlayerPrefs.SetInt("Coins", newCoins);
                PlayerPrefs.SetInt(gegenstand + index, 1);
                GameObject.FindGameObjectsWithTag("CoinManager")[0].GetComponent<CoinsAnzeige>().CoinUpdate();
                GameObject.FindGameObjectsWithTag("CoinManager")[1].GetComponent<CoinsAnzeige>().CoinUpdate();
            }
            if (PlayerPrefs.GetInt(gegenstand + index) == 1)
            {
                FindObjectOfType<AudioManager>().PlayRandomOfKind("ButtonHit2_", 7);
                coinLabel.active = false;
                //strich.active = true;
            }
        }
    }
}

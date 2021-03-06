﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionKachel : MonoBehaviour
{
    public int index;
    private int spielerIndex;

    GameObject[] holz;
    GameObject[] fischernetz;
    GameObject[] coins;

    public GameObject strudelAnimation;
    private GameObject strudelAnimationTemp;

    public GameObject rippleAnimation;
    public GameObject ripple;

    public GameObject WaterSplashObject;
    private GameObject WaterSplash;

    private bool winBool = false;

    //Wenn der Spieler auf eines der options Felder geklickt hat, werden alle Gegenstände, das Paket und der Spieler ein Feld weiter gesetzt
    private void OnMouseDown()
    {
        if (GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().finishedAnimating && !GameObject.Find("GameManager").GetComponent<GameManager>().dialogueActive)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().PlayerStuck();

            //Sound
            FindObjectOfType<AudioManager>().PlayRandomOfKind("WaterSplash2_", 7);

            DestroyImmediate(ripple, true);
            ripple = Instantiate(rippleAnimation, transform.position, Quaternion.Euler(0, 0, 0));

            spielerIndex = GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index;

            if (GameObject.Find("Kachel " + spielerIndex).GetComponent<Kachel>().altFlowBool)
            {
                GameObject.Find("Kachel " + spielerIndex).GetComponent<Kachel>().ChangePfeil();
            }

            //Strudel
            if (GameObject.Find("Kachel " + index).GetComponent<Kachel>().strudelBool)
            {
                DestroyImmediate(strudelAnimationTemp, true);
                strudelAnimationTemp = Instantiate(strudelAnimation, GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow.transform.position, Quaternion.Euler(0, 0, 0));

                GameObject.FindGameObjectWithTag("Spieler").transform.position = GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow.transform.position;
                //GameObject.FindGameObjectWithTag("Spieler").transform.position = GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow.transform.position;
                index = GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow.GetComponent<Kachel>().index;

                GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().ende = GameObject.FindGameObjectWithTag("Spieler").transform.position;
                GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().start = GameObject.FindGameObjectWithTag("Spieler").transform.position;
            }
            else
            {
                GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().finishedAnimating = false;
                GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().ende = transform.position;
            }

            //GameObject.FindGameObjectWithTag("Spieler").transform.rotation = Quaternion.Euler(0, 0, GameObject.Find("Kachel " + index).GetComponent<Kachel>().flowAngle);

            //Wenn das angeklickte Feld ein Fischernetz Feld ist werden alle Objekte außer dem Spieler un dem Fischernetz zwei Felder weiter bewegt
            if (GameObject.Find("Kachel " + index).GetComponent<Kachel>().fischernetz)
            {
                NextStep();

                //Fischernetz
                fischernetz = GameObject.FindGameObjectsWithTag("Fischernetz");
                if (fischernetz != null)
                {
                    for (int i = 0; i < fischernetz.Length; i++)
                    {
                        fischernetz[i].GetComponent<GegenstandBewegung>().GetToNextField();
                        //fischernetz[i].GetComponent<GegenstandBewegung>().next = GameObject.Find("Kachel " + fischernetz[i].GetComponent<GegenstandBewegung>().index);
                        GameObject.Find("Kachel " + fischernetz[i].GetComponent<GegenstandBewegung>().index).GetComponent<Kachel>().fischernetz = false;
                    }
                }

                NextStep();
            }
            else
            {
                //Fischernetz
                GameObject[] fischernetz = GameObject.FindGameObjectsWithTag("Fischernetz");
                if (fischernetz != null)
                {
                    for (int i = 0; i < fischernetz.Length; i++)
                    {
                        if(fischernetz[i].GetComponent<GegenstandBewegung>().index != GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().index)
                        {
                            fischernetz[i].GetComponent<GegenstandBewegung>().GetToNextField();
                            //fischernetz[i].GetComponent<GegenstandBewegung>().next = GameObject.Find("Kachel " + fischernetz[i].GetComponent<GegenstandBewegung>().index);
                            GameObject.Find("Kachel " + fischernetz[i].GetComponent<GegenstandBewegung>().index).GetComponent<Kachel>().fischernetz = false;
                        }
                    }
                }

                NextStep();
            }

            //Strudelfeld wird auf clear gesetzt und es wird ein neues Feld als Teleport Ziel gewählt
            for (int j = 1; j < GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().anzahlKacheln + 1; j++)
            {
                if (GameObject.Find("Kachel " + j).GetComponent<Kachel>().strudelBool)
                {
                    GameObject.Find("Kachel " + j).GetComponent<Kachel>().clear = true;
                    GameObject.Find("Kachel " + j).GetComponent<Kachel>().PickRandomFlow();
                }
            }

            //Überprüfung ob der Spieler auf einem Coin Feld ist
            coins = GameObject.FindGameObjectsWithTag("Coin");
            for (int i = 0; i < coins.Length; i++)
            {
                if (GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index == coins[i].GetComponent<GegenstandBewegung>().index)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
                    GameObject.Find("GameManager").GetComponent<GameManager>().ShowCoins();
                    coins[i].GetComponent<GegenstandBewegung>().CoinExplosion();
                }
            }


            //Überprüfung ob der Spieler gewonnen hat (ob er auf dem gleichen Feld wie das Paket steht)
            if (index == GameObject.FindGameObjectWithTag("Paket").GetComponent<GegenstandBewegung>().index)
            {
                PlayerPrefs.SetInt("Zuganzahl", GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetZuege());
                PlayerPrefs.SetInt("NextScene", GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().level);
                PlayerPrefs.SetInt("maxZuege", GameObject.Find("GameManager").GetComponent<GameManager>().maxZuege);
                PlayerPrefs.SetInt("Level0" + GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().level + "Solved", 1);

                winBool = true;

                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().OpenSceneWithDelay("Win");
            }


            //Überprüfung ob der Spieler verloren hat (ob er keine Züge mehr übrig hat)
            if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetZuege() < 1 && !winBool)
            {
                PlayerPrefs.SetInt("NextScene", GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().level);
                SceneManager.LoadScene("Loose", LoadSceneMode.Additive);
            }
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().ShowZuege();


            if (!GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().finishedAnimating && WaterSplash == null)
            {
                WaterSplash = Instantiate(WaterSplashObject, GameObject.FindGameObjectWithTag("Spieler").transform.position, Quaternion.Euler(0, 0, 0));
            }

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DestroyObjects("WaterSplashes");
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DestroyObjects("ripple");
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DestroyObjects("CoinExplosion");

            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().CheckGegenstaende();
            
        }
    }





    private void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().finishedAnimating && WaterSplash != null)
        {
            WaterSplash.transform.position = GameObject.FindGameObjectWithTag("Spieler").transform.position;
        }
    }


    //Setzt das Holz und das Paket ein Feld weiter
    private void NextStep()
    {
        holz = GameObject.FindGameObjectsWithTag("Holzplanke");
        if (holz != null)
        {
            for (int i = 0; i < holz.Length; i++)
            {
                holz[i].GetComponent<GegenstandBewegung>().GetToNextField();
                //GameObject.Find("Kachel " + holz[i].GetComponent<GegenstandBewegung>().index).GetComponent<Kachel>().clear = true;
            }
        }

        coins = GameObject.FindGameObjectsWithTag("Coin");
        if(coins != null)
        {
            for(int i = 0; i < coins.Length; i++)
            {
                coins[i].GetComponent<GegenstandBewegung>().GetToNextField();
                //GameObject.Find("Kachel " + coins[i].GetComponent<GegenstandBewegung>().index).GetComponent<Kachel>().clear = true;
            }
        }

        GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index = index;
        GameObject.FindGameObjectWithTag("Paket").GetComponent<GegenstandBewegung>().GetToNextField();
        GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().ShowOptions();
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SetZuege(-1);

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().spielerIndex = index;
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().paketIndex = GameObject.FindGameObjectWithTag("Paket").GetComponent<GegenstandBewegung>().index;
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SetBelegteFelder();
    }

}

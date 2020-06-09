using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionKachel : MonoBehaviour
{
    public int index;
    private int spielerIndex;


    public GameObject strudelAnimation;
    private GameObject strudel1, strudel2;

    AudioSource splashSound;

    //Wenn der Spieler auf eines der options Felder geklickt hat, werden alle Gegenstände, das Paket und der Spieler ein Feld weiter gesetzt
    private void OnMouseDown()
    {
        if (GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().finishedAnimating)
        {
            //Sound
            splashSound = GameObject.Find("SplashSound").GetComponent<AudioSource>();
            splashSound.Play(0);

            spielerIndex = GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index;

            if (GameObject.Find("Kachel " + spielerIndex).GetComponent<Kachel>().altFlowBool)
            {
                GameObject temp = GameObject.Find("Kachel " + spielerIndex).GetComponent<Kachel>().flow;
                GameObject.Find("Kachel " + spielerIndex).GetComponent<Kachel>().flow = GameObject.Find("Kachel " + spielerIndex).GetComponent<Kachel>().altFlow;
                GameObject.Find("Kachel " + spielerIndex).GetComponent<Kachel>().altFlow = temp;

                GameObject.Find("Kachel " + spielerIndex).GetComponent<Kachel>().ChangePfeil();
            }


            //Holz
            GameObject[] holz = GameObject.FindGameObjectsWithTag("Holzplanke");
            if (holz != null)
            {
                for (int i = 0; i < holz.Length; i++)
                {
                    holz[i].GetComponent<GegenstandBewegung>().GetToNextField();
                    GameObject.Find("Kachel " + holz[i].GetComponent<GegenstandBewegung>().index).GetComponent<Kachel>().clear = true;
                }
            }





            if (GameObject.Find("Kachel " + index).GetComponent<Kachel>().strudelBool)
            {
                DestroyImmediate(strudel1, true);
                DestroyImmediate(strudel2, true);
                strudel1 = Instantiate(strudelAnimation, transform.position, Quaternion.Euler(0, 0, 0));
                strudel2 = Instantiate(strudelAnimation, GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow.transform.position, Quaternion.Euler(0, 0, 0));


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

            //GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().spielerIndex = index;

            if (GameObject.Find("Kachel " + index).GetComponent<Kachel>().fischernetz)
            {
                Debug.Log("Tada");

                holz = GameObject.FindGameObjectsWithTag("Holzplanke");
                if (holz != null)
                {
                    for (int i = 0; i < holz.Length; i++)
                    {
                        holz[i].GetComponent<GegenstandBewegung>().GetToNextField();
                        GameObject.Find("Kachel " + holz[i].GetComponent<GegenstandBewegung>().index).GetComponent<Kachel>().clear = true;
                    }
                }


                GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index = index;
                GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().GetToNextField();
                GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().ShowOptions();
                GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().SetZuege(-1);
                GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().paketIndex = GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().index;
                GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().SetBelegteFelder();


                GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().GetToNextField();
                GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().ShowOptions();
                GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().SetZuege(-1);
                GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().paketIndex = GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().index;
                GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().SetBelegteFelder();


                //Fischernetz
                GameObject[] fischernetz = GameObject.FindGameObjectsWithTag("Fischernetz");
                if (fischernetz != null)
                {
                    for (int i = 0; i < fischernetz.Length; i++)
                    {
                        
                        fischernetz[i].GetComponent<GegenstandBewegung>().GetToNextField();
                        fischernetz[i].GetComponent<GegenstandBewegung>().next = GameObject.Find("Kachel " + fischernetz[i].GetComponent<GegenstandBewegung>().index);
                        GameObject.Find("Kachel " + fischernetz[i].GetComponent<GegenstandBewegung>().index).GetComponent<Kachel>().fischernetz = false;
                    }
                }
            }
            else
            {

                //Fischernetz
                GameObject[] fischernetz = GameObject.FindGameObjectsWithTag("Fischernetz");
                if (fischernetz != null)
                {
                    for (int i = 0; i < fischernetz.Length; i++)
                    {

                        fischernetz[i].GetComponent<GegenstandBewegung>().GetToNextField();
                        GameObject.Find("Kachel " + fischernetz[i].GetComponent<GegenstandBewegung>().index).GetComponent<Kachel>().fischernetz = false;
                    }
                }


                GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index = index;
                GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().GetToNextField();
                GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().ShowOptions();
                GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().SetZuege(-1);

                GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().spielerIndex = index;
                GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().paketIndex = GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().index;
                GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().SetBelegteFelder();
            }

            //Strudelfeld wird auf clear gesetzt und es wird ein neues Feld als Teleport Ziel gewählt
            for (int j = 1; j < 38; j++)
            {
                if (GameObject.Find("Kachel " + j).GetComponent<Kachel>().strudelBool)
                {
                    GameObject.Find("Kachel " + j).GetComponent<Kachel>().clear = true;
                    GameObject.Find("Kachel " + j).GetComponent<Kachel>().PickRandomFlow();
                }
            }



            if (GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().GetZuege() < 1)
            {
                Debug.Log("VERLOREN!");
                PlayerPrefs.SetInt("NextScene", GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().level);
                SceneManager.LoadScene("Loose", LoadSceneMode.Single);
            }
            GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().ShowZuege();


            //Überprüfung ob der Spieler gewonnen hat (ob er auf dem gleichen Feld wie das Paket steht)
            if (GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index == GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().index)
            {
                Debug.Log("GEWONNEN!");
                PlayerPrefs.SetInt("Zuganzahl", GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().GetZuege());
                PlayerPrefs.SetInt("NextScene", GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().level);
                SceneManager.LoadScene("Win", LoadSceneMode.Single);
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int anzahlKacheln;
    public bool tutorial;

    public int spielerIndex;
    public int spielerStartIndex;

    public int paketIndex;
    public int paketStartIndex;
    private int[] paketRandomStartSpots = new int[] {26, 27, 28, 31, 32, 33, 35, 36, 37};

    public int anzahlHolz;
    private int[] holzStartIndex;
    public GameObject[] holz;

    public int anzahlFischernetz;
    private int[] fischernetzStartIndex;
    public GameObject[] fischernetz;

    public int anzahlCoins;
    private int[] coinsStartIndex;
    public GameObject[] coins;

    private int[] gegenstandSpots;
    public int[] belegteFelder;

    public GameObject spieler;
    public GameObject paket;
    public GameObject holzplanke;
    public GameObject fischernetzObject;
    public GameObject coinsObject;

    GameObject parent;

    public Text zuegeLabel;
    public int maxZuege;
    public int zuege;

    public Text levelLabel;
    public int level;

    public Text coinsLabel;

    public bool dialogueActive = false;


    //Initiiert alle Spielelemente
    void Start()
    {
        zuege = maxZuege;
        levelLabel.GetComponent<Text>().text = "Level " + level;
        GegenstandSpotsInit();
        GameInit();
        zuege = maxZuege;
        ShowZuege();
        belegteFelder = gegenstandSpots;

        FindObjectOfType<AudioManager>().Play("BackgroundWaves");
        //FindObjectOfType<AudioManager>().Play("Theme");

        ShowCoins();

        if (tutorial)
        {
            dialogueActive = true;
            StartCoroutine(OpenDialogue());
        }

        PlayerPrefs.SetInt("aktuellesLevel", level);
        PlayerPrefs.SetInt("Menue", 0);
    }

    IEnumerator OpenDialogue()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("DialogTrigger").GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    //Instantiiert den Spieler, das Paket und die Holzplanken auf dem Spielfeld 
    void GameInit()
    {
        //Holz
        holz = new GameObject[anzahlHolz];
        
        holzStartIndex = new int[anzahlHolz];
        holzStartIndex = PickSpots(anzahlHolz);

        for (int i = 0; i < holz.Length; i++)
        {
            parent = GameObject.Find("Kachel " + holzStartIndex[i]);
            holz[i] = Instantiate(holzplanke, parent.transform.position, Quaternion.Euler(0, 0, 0));
            holz[i].GetComponent<GegenstandBewegung>().index = holzStartIndex[i];
        }


        //Fischernetz
        fischernetz = new GameObject[anzahlFischernetz];

        fischernetzStartIndex = new int[anzahlFischernetz];
        fischernetzStartIndex = PickSpots(anzahlFischernetz);

        for (int i = 0; i < fischernetz.Length; i++)
        {
            parent = GameObject.Find("Kachel " + fischernetzStartIndex[i]);
            fischernetz[i] = Instantiate(fischernetzObject, parent.transform.position, Quaternion.Euler(0, 0, 0));
            fischernetz[i].GetComponent<GegenstandBewegung>().index = fischernetzStartIndex[i];
        }


        //Coins
        coins = new GameObject[anzahlCoins];

        coinsStartIndex = new int[anzahlCoins];
        coinsStartIndex = PickSpots(anzahlCoins);

        for(int i = 0; i < anzahlCoins; i++)
        {
            parent = GameObject.Find("Kachel " + coinsStartIndex[i]);
            coins[i] = Instantiate(coinsObject, parent.transform.position, Quaternion.Euler(0, 0, 0));
            coins[i].GetComponent<GegenstandBewegung>().index = coinsStartIndex[i];
        }


        //Paket Instantiierung
        parent = GameObject.Find("Kachel " + paketStartIndex);
        Instantiate(paket, parent.transform.position, Quaternion.Euler(0, 0, 0));
        paketIndex = paketStartIndex;

        //Spieler Instantiierung
        parent = GameObject.Find("Kachel " + spielerStartIndex);
        Instantiate(spieler, parent.transform.position, Quaternion.Euler(0, 0, 0));
        spielerIndex = spielerStartIndex;
    }

    //Initiiert das globale Array, welches die freien Felder auf dem Spielfeld enthält
    void GegenstandSpotsInit()
    {
        gegenstandSpots = new int[anzahlKacheln + 1];

        for (int i = 0; i < gegenstandSpots.Length; i++)
        {
            gegenstandSpots[i] = i;
        }

        gegenstandSpots[spielerStartIndex] = 0;

        if (!tutorial)
        {
            int rand = Random.Range(0, paketRandomStartSpots.Length);
            paketStartIndex = paketRandomStartSpots[rand];
        }

        gegenstandSpots[paketStartIndex] = 0;

        string[] nameO = GameObject.Find("Kachel " + paketStartIndex).GetComponent<Kachel>().GetFlow((int)GameObject.Find("Kachel " + paketStartIndex).GetComponent<Kachel>().transform.rotation.eulerAngles.z).GetComponent<Kachel>().name.Split(' ');

        int tempIndex = int.Parse(nameO[1]);
        gegenstandSpots[tempIndex] = 0;


        for(int j = 1; j < anzahlKacheln + 1; j++)
        {
            if(GameObject.Find("Kachel " + j).GetComponent<Kachel>().stoneBool)
            {
                gegenstandSpots[j] = 0;
            }
        }

        for(int k = 1; k < anzahlKacheln + 1; k++)
        {
            if(GameObject.Find("Kachel " + k).GetComponent<Kachel>().strudelBool)
            {
                gegenstandSpots[k] = 0;
                for (int l = 0; l < 6; l++)
                {
                    gegenstandSpots[GameObject.Find("Kachel " + k).GetComponent<Kachel>().neighbours[l].GetComponent<Kachel>().index] = 0;
                }
            }
        }
    }


    //Gibt ein Array der Länge "anzahl" mit indices der freien Kacheln für Gegenstände zurück
    private int[] PickSpots(int anzahl)
    {
        int[] spots = new int[anzahl];
        int rand;

        int j = 0;
        while (j < anzahl)
        {
            rand = Random.Range(1, anzahlKacheln);

            if (gegenstandSpots[rand] != 0)
            {
                spots[j] = rand;
                gegenstandSpots[rand] = 0;

                string[] nameO = GameObject.Find("Kachel " + rand).GetComponent<Kachel>().GetFlow((int)GameObject.Find("Kachel " + rand).GetComponent<Kachel>().transform.rotation.eulerAngles.z).GetComponent<Kachel>().name.Split(' ');

                int tempIndex = int.Parse(nameO[1]);
                gegenstandSpots[tempIndex] = 0;

                j++;
            }
        }
        return spots;
    }


    //Zuganzeige
    public void SetZuege(int value)
    {
        zuege += value;
    }

    public int GetZuege()
    {
        return zuege;
    }

    public void ShowZuege()
    {
        if (zuege > 5)
        {
            zuegeLabel.GetComponent<Text>().text = "Verbleibende Züge: " + zuege;
        }
        else
        {
            zuegeLabel.GetComponent<Text>().text = "Verbleibende Züge: " + "<color=#ff0000ff>" +zuege +"</color>";
        }
    }

    //Coins Anzeige
    public void ShowCoins()
    {
        coinsLabel.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("Coins");
    }


    //Setzt alle belegten Felder im globalen Array auf 0 (Fischernetzt sind davon ausgenommen
    public void SetBelegteFelder()
    {
        for(int i = 0; i < belegteFelder.Length; i++)
        {
            belegteFelder[i] = i;
        }

        belegteFelder[spielerIndex] = 0;
        belegteFelder[paketIndex] = 0;
        belegteFelder[GameObject.FindGameObjectWithTag("Paket").GetComponent<GegenstandBewegung>().next.GetComponent<Kachel>().index] = 0;


        //Steine
        for (int j = 1; j < anzahlKacheln + 1; j++)
        {
            if (GameObject.Find("Kachel " + j).GetComponent<Kachel>().stoneBool)
            {
                gegenstandSpots[j] = 0;
            }
        }


        //Spieler Options
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().options.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().options[i] != null)
            {
                belegteFelder[GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().options[i].GetComponent<Kachel>().index] = 0;
            }
        }


        //Holzbretter
        GameObject[] Hoelzer = GameObject.FindGameObjectsWithTag("Holzplanke");
        for (int j = 0; j < Hoelzer.Length; j++)
        {
            belegteFelder[Hoelzer[j].GetComponent<GegenstandBewegung>().index] = 0;
            belegteFelder[Hoelzer[j].GetComponent<GegenstandBewegung>().next.GetComponent<Kachel>().index] = 0;
        }


        //Coins
        GameObject[] Coins = GameObject.FindGameObjectsWithTag("Coin");
        for (int j = 0; j < Coins.Length; j++)
        {
            belegteFelder[Coins[j].GetComponent<GegenstandBewegung>().index] = 0;
            belegteFelder[Coins[j].GetComponent<GegenstandBewegung>().next.GetComponent<Kachel>().index] = 0;
        }


        //Strudelfeld und alle Neighbours vom Strudelfeld
        for (int k = 1; k < anzahlKacheln + 1; k++)
        {
            if (GameObject.Find("Kachel " + k).GetComponent<Kachel>().strudelBool)
            {
                gegenstandSpots[k] = 0;
                for (int l = 0; l < 6; l++)
                {
                    gegenstandSpots[GameObject.Find("Kachel " + k).GetComponent<Kachel>().neighbours[l].GetComponent<Kachel>().index] = 0;
                }
            }
        }
    }

    public void PlayerStuck()
    {
        StartCoroutine(PlayerStuckCheck());
    }

    IEnumerator PlayerStuckCheck()
    {
        yield return new WaitForSeconds(2f);
        if(GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().options == null)
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().TransitionToNextLevel("Loose");
        }
    }

   public void DestroyObjects(string name)
    {
        StartCoroutine(DelayDestroy(name));
    }


    IEnumerator DelayDestroy(string name)
    {
        yield return new WaitForSeconds(1f);
        GameObject[] water = GameObject.FindGameObjectsWithTag(name);
        foreach (GameObject w in water)
        {
            DestroyImmediate(w, true);
        }
    }



    public void CheckGegenstaende()
    {
        //GameObject[] alleGegenstaende = GameObject.FindObjectsOfType<GegenstandBewegung>();
        GegenstandBewegung[] alleGegenstaende = (GegenstandBewegung[])GameObject.FindObjectsOfType(typeof(GegenstandBewegung));
        

        bool liegtUebereinander = true;
        while (liegtUebereinander)
        {
            liegtUebereinander = false;
            for (int i = 0; i < alleGegenstaende.Length; i++)
            {
                for(int j = 0; j < alleGegenstaende.Length; j++)
                {
                    if(alleGegenstaende[i].GetComponent<GegenstandBewegung>().next.GetComponent<Kachel>().index == alleGegenstaende[j].GetComponent<GegenstandBewegung>().next.GetComponent<Kachel>().index && i != j)
                    {
                        Debug.Log("Liegt Übereinadner");
                        Debug.Log(alleGegenstaende[i].GetComponent<GegenstandBewegung>().index + "" + alleGegenstaende[i] + "" + alleGegenstaende[j].GetComponent<GegenstandBewegung>().index + "" + alleGegenstaende[j]);
                        Debug.Log(alleGegenstaende[i].GetComponent<GegenstandBewegung>().next.GetComponent<Kachel>().index);
                        alleGegenstaende[i].GetComponent<GegenstandBewegung>().StepBack();
                        Debug.Log(alleGegenstaende[i].GetComponent<GegenstandBewegung>().next.GetComponent<Kachel>().index);
                        liegtUebereinander = true;
                    }
                }
            }
        }


        //for (int i = 0; i < alleGegenstaende.Length; i++) {
        //    Debug.Log(alleGegenstaende[i]);
        //    Debug.Log(alleGegenstaende[i].GetComponent<GegenstandBewegung>().index);
        //}




        //Controller[] myItems = FindObjectsOfType(typeof(Controller)) as Controller[];
        //Debug.Log("Found " + myItems.Length + " instances with this script attached");
        //foreach (Controller item in myItems)
        //{
        //    Debug.Log(item.gameObject.name);
        //}
    }
}

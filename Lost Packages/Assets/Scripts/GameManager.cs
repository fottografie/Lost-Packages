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

    private int[] gegenstandSpots;
    public int[] belegteFelder;

    public GameObject spieler;
    public GameObject paket;
    public GameObject holzplanke;
    public GameObject fischernetzObject;

    public Text zuegeLabel;
    public int zuege;

    public Text levelLabel;
    public int level;

    AudioSource backgroundSound;


    // Start is called before the first frame update
    void Start()
    {
        levelLabel.GetComponent<Text>().text = "Level " + level;
        GegenstandSpotsInit();
        GameInit();
        ShowZuege();
        paketIndex = paketStartIndex;
        spielerIndex = spielerStartIndex;
        belegteFelder = gegenstandSpots;

        backgroundSound = GameObject.Find("Soundtrack").GetComponent<AudioSource>();
        backgroundSound.Play(0);
    }


    //Instantiert den Spieler, das Paket und die Holzplanken auf dem Spielfeld 
    void GameInit()
    {
        //Holz
        holz = new GameObject[anzahlHolz];
        GameObject parent;

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


        parent = GameObject.Find("Kachel " + paketStartIndex);
        Instantiate(paket, parent.transform.position, Quaternion.Euler(0, 0, 0));
        GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().index = paketStartIndex;

        parent = GameObject.Find("Kachel " + spielerStartIndex);
<<<<<<< Updated upstream
        Instantiate(spieler, parent.transform.position, parent.transform.rotation);
=======
        Instantiate(spieler, parent.transform.position, Quaternion.Euler(0, 0, 0));
>>>>>>> Stashed changes
        GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index = paketStartIndex;
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
            //gegenstandSpots[paketStartIndex] = 0;
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



    public void SetBelegteFelder()
    {
        for(int i = 0; i < belegteFelder.Length; i++)
        {
            belegteFelder[i] = i;
        }

        belegteFelder[spielerIndex] = 0;
        belegteFelder[paketIndex] = 0;
        belegteFelder[GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().next.GetComponent<Kachel>().index] = 0;


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


        //Fischernetze
        //GameObject[] Fischernetze = GameObject.FindGameObjectsWithTag("Fischernetz");
        //for (int j = 0; j < Fischernetze.Length; j++)
        //{
        //    belegteFelder[Fischernetze[j].GetComponent<GegenstandBewegung>().index] = 0;
        //    belegteFelder[Fischernetze[j].GetComponent<GegenstandBewegung>().next.GetComponent<Kachel>().index] = 0;
        //}
    }

}

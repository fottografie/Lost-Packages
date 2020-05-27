using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielerBewegung : MonoBehaviour
{
    public GameObject optionKachel;
    public GameObject optionPfeil;

    public GameObject[] options = new GameObject[3];
    private GameObject[] felder = new GameObject[3];

    public int index;
    public int minimumRounds;
    private GameObject[] minimumOptions;

    private GameObject[] holzKachel;


    // Start is called before the first frame update
    void Start()
    {
        index = GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().spielerStartIndex;
        ShowOptions();


        //holzKachel = new GameObject[3];
        //
        //for(int i = 0; i < holzKachel.Length; i++)
        //{
        //    holzKachel[i] = GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().holz[i];
        //
        //}


        //GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().MinimumRounds(GameObject.Find("Kachel " + GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().paketStartIndex).GetComponent<Kachel>().flow, holzKachel);
        //minimumRounds = GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().depth;
        //minimumOptions = new GameObject[GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().options.Count];
        //for(int i = 0; i < minimumOptions.Length; i++)
        //{
        //    minimumOptions[i] = GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().options[i];
        //}
    }

    //Entfernt alle grünen options Felder
    public void DestroyAllOptional()
    {
        GameObject[] optional = GameObject.FindGameObjectsWithTag("optionKachel");
        for (int i = 0; i < optional.Length; i++)
        {
            DestroyImmediate(optional[i], true);
        }
    }

    //Markiert alle Felder (+ Pfeile) auf die der Spieler ziehen kann (wenn diese nicht von Gegenständen belegt sind)
    public void ShowOptions()
    {
        options = GameObject.Find("Kachel " + index).GetComponent<Kachel>().PlayerNextField();
        //Debug.Log(GameObject.Find("Kachel " + index).GetComponent<Kachel>().flowAngle);
        float angle = GameObject.Find("Kachel " + index).GetComponent<Kachel>().flowAngle;
        GameObject[] felder = new GameObject[3];

        DestroyAllOptional();

        //Option-Pfeil + -Kachel rechts der Flussrichtung
        if((options[0] != null && options[0].GetComponent<Kachel>().clear) || (options[0] != null && options[0].GetComponent<Kachel>().package))
        {
            felder[0] = Instantiate(optionKachel, options[0].transform.position, Quaternion.Euler(0, 0, 0));
            felder[0].GetComponent<optionKachel>().index = options[0].GetComponent<Kachel>().index;
            float pi = (angle + 300) % 360;
            Instantiate(optionPfeil, transform.position, Quaternion.Euler(0, 0, pi));
        }

        //Option-Pfeil + -Kachel in der Flussrichtung
        if ((options[1] != null && options[1].GetComponent<Kachel>().clear) || (options[1] != null && options[1].GetComponent<Kachel>().package))
        {
            felder[1] = Instantiate(optionKachel, options[1].transform.position, Quaternion.Euler(0, 0, 0));
            felder[1].GetComponent<optionKachel>().index = options[1].GetComponent<Kachel>().index;
            Instantiate(optionPfeil, transform.position, Quaternion.Euler(0, 0, angle));
        }

        //Option-Pfeil + -Kachel links der Flussrichtung
        if ((options[2] != null && options[2].GetComponent<Kachel>().clear) || (options[2] != null && options[2].GetComponent<Kachel>().package))
        {
            felder[2] = Instantiate(optionKachel, options[2].transform.position, Quaternion.Euler(0, 0, 0));
            felder[2].GetComponent<optionKachel>().index = options[2].GetComponent<Kachel>().index;
            float phi = (angle + 60) % 360;
            Instantiate(optionPfeil, transform.position, Quaternion.Euler(0, 0, phi));
        }


    }



}
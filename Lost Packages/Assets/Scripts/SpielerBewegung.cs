using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielerBewegung : MonoBehaviour
{
    public GameObject optionKachel;
    public GameObject optionPfeil;

    public GameObject[] options = new GameObject[3];

    public int index;
    public int minimumRounds;

    public Vector3 start;
    public Vector3 ende;
    public Vector3 richtung;

    public bool finishedAnimating = true;


    void Start()
    {
        index = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().spielerStartIndex;
        start = transform.position;
        ende = transform.position;
        ShowOptions();
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
        float angle = GameObject.Find("Kachel " + index).GetComponent<Kachel>().flowAngle;
        GameObject[] felder = new GameObject[3];

        DestroyAllOptional();

        //Option-Pfeil + -Kachel rechts der Flussrichtung
        if((options[0] != null && options[0].GetComponent<Kachel>().clear && !options[0].GetComponent<Kachel>().stoneBool) || (options[0] != null && options[0].GetComponent<Kachel>().package && !options[0].GetComponent<Kachel>().stoneBool))
        {
            felder[0] = Instantiate(optionKachel, options[0].transform.position, Quaternion.Euler(0, 0, 0));
            felder[0].GetComponent<OptionKachel>().index = options[0].GetComponent<Kachel>().index;
            float pi = (angle + 300) % 360;

            Instantiate(optionPfeil, GameObject.Find("Kachel " + index).GetComponent<Kachel>().transform.position, Quaternion.Euler(0, 0, pi));
        }

        //Option-Pfeil + -Kachel in der Flussrichtung
        if ((options[1] != null && options[1].GetComponent<Kachel>().clear && !options[1].GetComponent<Kachel>().stoneBool) || (options[1] != null && options[1].GetComponent<Kachel>().package && !options[1].GetComponent<Kachel>().stoneBool))
        {
            felder[1] = Instantiate(optionKachel, options[1].transform.position, Quaternion.Euler(0, 0, 0));
            felder[1].GetComponent<OptionKachel>().index = options[1].GetComponent<Kachel>().index;

            Instantiate(optionPfeil, GameObject.Find("Kachel " + index).GetComponent<Kachel>().transform.position, Quaternion.Euler(0, 0, angle));
        }

        //Option-Pfeil + -Kachel links der Flussrichtung
        if ((options[2] != null && options[2].GetComponent<Kachel>().clear && !options[2].GetComponent<Kachel>().stoneBool) || (options[2] != null && options[2].GetComponent<Kachel>().package && !options[2].GetComponent<Kachel>().stoneBool))
        {
            felder[2] = Instantiate(optionKachel, options[2].transform.position, Quaternion.Euler(0, 0, 0));
            felder[2].GetComponent<OptionKachel>().index = options[2].GetComponent<Kachel>().index;
            float phi = (angle + 60) % 360;
            Instantiate(optionPfeil, GameObject.Find("Kachel " + index).GetComponent<Kachel>().transform.position, Quaternion.Euler(0, 0, phi));
        }
    }

    //Für Spieler Animation
    private void FixedUpdate()
    {
        if (start != ende)
        {
            start = ZugAnimation();
            transform.position = start;
        }
        else
        {
            finishedAnimating = true;
        }
    }

    //Spieler Animation
    Vector3 ZugAnimation()
    {
        richtung = ende - start;
        richtung = Vector3.ClampMagnitude(richtung, 0.05f);

        Vector3 newStart;
        newStart = start + richtung;

        return newStart;
    }
}
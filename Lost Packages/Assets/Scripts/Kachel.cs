using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kachel : MonoBehaviour
{
    public int index;
    public bool clear;
    public bool package;

    public GameObject pfeil;
    public GameObject[] neighbours = new GameObject[6];

    public GameObject flow;

    // Start is called before the first frame update
    void Start()
    {
        SetIndex();
        Instantiate(pfeil, transform.position, transform.rotation);
        GetFlow();
    }

    //Setzt den Index der Kachel auf den Namen des GameObjects
    void SetIndex()
    {
        string[] nameO = gameObject.name.Split(' ');
        index = int.Parse(nameO[1]);
        clear = true;
        package = false;
    }


    //Berechnet abhängig von der Drehung der Kachel das nächste Feld in Flussrichtung
    void GetFlow()
    {
        if (transform.eulerAngles.z == 0)
        {
            flow = neighbours[3];
        }
        else if (transform.eulerAngles.z > 59 && transform.eulerAngles.z < 61)
        {
            flow = neighbours[1];
        }
        else if (transform.eulerAngles.z > 119 && transform.eulerAngles.z < 121)
        {
            flow = neighbours[0];
        }
        else if (transform.eulerAngles.z == 180)
        {
            flow = neighbours[2];
        }
        else if (transform.eulerAngles.z == 240)
        {
            flow = neighbours[4];
        }
        else if (transform.eulerAngles.z == 300)
        {
            flow = neighbours[5];
        }

    }


    //Berechnet anhandt der Drehnung der Kachel die Optionsfelder des Spielers
    public GameObject[] PlayerNextField()
    {
        GameObject[] playerNextField = new GameObject[3];
        if (transform.eulerAngles.z == 0)
        {
            playerNextField[0] = neighbours[5];
            playerNextField[1] = neighbours[3];
            playerNextField[2] = neighbours[1];
        }
        else if (transform.eulerAngles.z > 59 && transform.eulerAngles.z < 61)
        {
            playerNextField[0] = neighbours[3];
            playerNextField[1] = neighbours[1];
            playerNextField[2] = neighbours[0];
        }
        else if (transform.eulerAngles.z > 119 && transform.eulerAngles.z < 121)
        {
            playerNextField[0] = neighbours[1];
            playerNextField[1] = neighbours[0];
            playerNextField[2] = neighbours[2];
        }
        else if (transform.eulerAngles.z == 180)
        {
            playerNextField[0] = neighbours[0];
            playerNextField[1] = neighbours[2];
            playerNextField[2] = neighbours[4];
        }
        else if (transform.eulerAngles.z == 240)
        {
            playerNextField[0] = neighbours[2];
            playerNextField[1] = neighbours[4];
            playerNextField[2] = neighbours[5];
        }
        else if (transform.eulerAngles.z == 300)
        {
            playerNextField[0] = neighbours[4];
            playerNextField[1] = neighbours[5];
            playerNextField[2] = neighbours[3];
        }
        return playerNextField;
    }

}

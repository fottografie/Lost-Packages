using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kachel : MonoBehaviour
{
    public GameObject pfeil;
    public int index;
    public GameObject[] neighbours = new GameObject[6];
    public GameObject flow;
    public GameObject[] playerNextField = new GameObject[3];
    public bool clear;

    // Start is called before the first frame update
    void Start()
    {
        clear = true;
        string[] nameO = gameObject.name.Split(' ');
        index = int.Parse(nameO[1]);
        _ = Instantiate(pfeil, transform.position, transform.rotation);
        GetFlow();
    }

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

    public GameObject[] PlayerNextField()
    {
        if (transform.eulerAngles.z == 0)
        {
            playerNextField[0] = neighbours[1];
            playerNextField[1] = neighbours[3];
            playerNextField[2] = neighbours[5];
        }
        else if (transform.eulerAngles.z > 59 && transform.eulerAngles.z < 61)
        {
            playerNextField[0] = neighbours[0];
            playerNextField[1] = neighbours[1];
            playerNextField[2] = neighbours[3];
        }
        else if (transform.eulerAngles.z > 119 && transform.eulerAngles.z < 121)
        {
            playerNextField[0] = neighbours[2];
            playerNextField[1] = neighbours[0];
            playerNextField[2] = neighbours[1];
        }
        else if (transform.eulerAngles.z == 180)
        {
            playerNextField[0] = neighbours[4];
            playerNextField[1] = neighbours[2];
            playerNextField[2] = neighbours[0];
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

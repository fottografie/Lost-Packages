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

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.eulerAngles.z);
        string[] nameO = gameObject.name.Split(' ');
        index = int.Parse(nameO[1]);
        _ = Instantiate(pfeil, transform.position, transform.rotation);
        GetFlow();

    }

    // Update is called once per frame
    void Update()
    {

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

    void GetNeighbours()
    {
        int reihe = 6;

        if (index > 0 && index < 5)
        {
            reihe = 4;
        }
        else if (index > 5 && index < 9)
        {
            reihe = 5;
        }
        else if (index > 10 && index < 15)
        {
            reihe = 6;
        }
        else if (index > 16 && index < 22)
        {
            reihe = 7;
        }
        else if (index > 23 && index < 28)
        {
            reihe = 6;
        }
        else if (index > 29 && index < 33)
        {
            reihe = 5;
        }
        else if (index > 33 && index < 38)
        {
            reihe = 4;
        }


        neighbours[0] = GameObject.Find("Kachel " + (index - reihe).ToString());
        neighbours[1] = GameObject.Find("Kachel " + (index - reihe + 1).ToString());
        neighbours[2] = GameObject.Find("Kachel " + (index - 1).ToString());
        neighbours[3] = GameObject.Find("Kachel " + (index + 1).ToString());
        neighbours[4] = GameObject.Find("Kachel " + (index + reihe).ToString());
        neighbours[5] = GameObject.Find("Kachel " + (index + reihe + 1).ToString());


    }


}

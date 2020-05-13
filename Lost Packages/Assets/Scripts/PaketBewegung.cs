﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaketBewegung : MonoBehaviour
{
    public int startIndex;
    public int index;
    public GameObject next;
    public GameObject old;
    public bool nextFieldClear;

    // Start is called before the first frame update
    void Start()
    {
        index = startIndex;
        
        GetFlowFromIndex();
    }

    void GetFlowFromIndex()
    {
        old = next;
        GameObject f = GameObject.Find("Kachel " + index);
        next = f.GetComponent<Kachel>().flow;
        if(next.GetComponent<Kachel>().clear == false)
        {
            next = old;
        }
        else
        {
            next.GetComponent<Kachel>().clear = false;
        }
    }


    public void GetToNextField()
    {
        transform.position = next.transform.position;
        index = next.GetComponent<Kachel>().index;
        GameObject.Find("Kachel " + index).GetComponent<Kachel>().clear = true;
        GetFlowFromIndex();
    }

}

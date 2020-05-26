using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaketBewegung : MonoBehaviour
{
    public int index;
    public GameObject next;
    private GameObject old;

    public GameObject belegtPaket;

    // Start is called before the first frame update
    void Start()
    {
        GetFlowFromIndex();
    }

    //Setzt next auf das Feld in Flussrichtung, wenn es nich von einem Gegenstand belegt ist und setzt das nächste Feld auf package = true
    void GetFlowFromIndex()
    {
        old = next;
        next = GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow;
        if (next.GetComponent<Kachel>().clear == false)
        {
            next = old;
        }
        next.GetComponent<Kachel>().clear = false;
        next.GetComponent<Kachel>().package = true;

        Instantiate(belegtPaket, next.transform.position, Quaternion.identity);
    }

    //Der Gegenstand wird auf das nächste Feld gesetzt, die alten belegten Kacheln werden entfernt und das GetFlowFromIndex wird erneut aufgerufen
    public void GetToNextField()
    {
        transform.position = next.transform.position;
        index = next.GetComponent<Kachel>().index;
        GameObject.Find("Kachel " + index).GetComponent<Kachel>().clear = true;
        GameObject.Find("Kachel " + index).GetComponent<Kachel>().package = false;
        
        DestroyImmediate(GameObject.FindGameObjectWithTag("belegtPaket"), true);

        GetFlowFromIndex();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GegenstandBewegung : MonoBehaviour
{
    public int index;
    public GameObject next;
    private GameObject old;

    public GameObject belegtHolzObject;
    private GameObject belegtHolz;

    // Start is called before the first frame update
    void Start()
    {
        GetFlowFromIndex();
    }

    //Setzt next auf das Feld in Flussrichtung, wenn es nich von einem Gegenstand belegt ist und instantiiert darauf ein belegt-Feld
    void GetFlowFromIndex()
    {
        next = GameObject.Find("Kachel " + index);
        old = next;
        next = GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow;
        if (next.GetComponent<Kachel>().clear == false)
        {
            next = old;
        }
        next.GetComponent<Kachel>().clear = false;

        belegtHolz = Instantiate(belegtHolzObject, next.transform.position, Quaternion.Euler(0, 0, 0));
    }

    //Der Gegenstand wird auf das nächste Feld gesetzt, die alten belegten Kacheln werden entfernt und das GetFlowFromIndex wird erneut aufgerufen
    public void GetToNextField()
    {
        if (GameObject.Find("Kachel " + index).GetComponent<Kachel>().altFlowBool)
        {
            GameObject temp = GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow;
            GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow = GameObject.Find("Kachel " + index).GetComponent<Kachel>().altFlow;
            GameObject.Find("Kachel " + index).GetComponent<Kachel>().altFlow = temp;

            GameObject.Find("Kachel " + index).GetComponent<Kachel>().ChangePfeil();
        }


        transform.position = next.transform.position;
        index = next.GetComponent<Kachel>().index;
        GameObject.Find("Kachel " + index).GetComponent<Kachel>().clear = true;

        DestroyImmediate(belegtHolz, true);

        GetFlowFromIndex();
    }

}

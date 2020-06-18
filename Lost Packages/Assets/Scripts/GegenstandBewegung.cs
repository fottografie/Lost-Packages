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

    public GameObject belegtHolzPfeilObject;
    private GameObject belegtHolzPfeil;

    public bool fischernetzBool;

    public Vector3 start;
    public Vector3 ende;
    public Vector3 richtung;

    public GameObject strudelAnimation;
    private GameObject strudel;


    // Start is called before the first frame update
    void Start()
    {
        GetFlowFromIndex();
        start = transform.position;
        ende = transform.position;
    }

    //Setzt next auf das Feld in Flussrichtung, wenn es nicht von einem Gegenstand belegt ist und instantiiert darauf ein belegt-Feld
    void GetFlowFromIndex()
    {
        next = GameObject.Find("Kachel " + index);
        old = next;
        next = GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow;
        if (!next.GetComponent<Kachel>().clear)
        {
            next = old;
        }
        if (!fischernetzBool)
        {
            next.GetComponent<Kachel>().clear = false;
        }
        else
        {
            GameObject.Find("Kachel " + index).GetComponent<Kachel>().fischernetz = false;
            next.GetComponent<Kachel>().fischernetz = true;
        }

        if (next != GameObject.Find("Kachel " + index) && !GameObject.Find("Kachel " + index).GetComponent<Kachel>().strudelBool && GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index != index)
        {
            belegtHolzPfeil = Instantiate(belegtHolzPfeilObject, GameObject.Find("Kachel " + index).transform.position, GameObject.Find("Kachel " + index).transform.rotation);
            //belegtHolz = Instantiate(belegtHolzObject, next.transform.position, Quaternion.Euler(0, 0, 0));
        }
        else if(next == GameObject.Find("Kachel " + index))
        {
            belegtHolz = Instantiate(belegtHolzObject, next.transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    //Der Gegenstand wird auf das nächste Feld gesetzt, die alten belegten Kacheln werden entfernt und GetFlowFromIndex wird erneut aufgerufen
    public void GetToNextField()
    {
        GameObject.Find("Kachel " + next.GetComponent<Kachel>().index).GetComponent<Kachel>().clear = true;

        //Alt Flow
        if (GameObject.Find("Kachel " + index).GetComponent<Kachel>().altFlowBool)
        {
            GameObject.Find("Kachel " + index).GetComponent<Kachel>().ChangePfeil();
        }

        //Strudel
        if (next.GetComponent<Kachel>().strudelBool)
        {
            transform.position = next.transform.position;
            DestroyImmediate(strudel, true);
            strudel = Instantiate(strudelAnimation, next.GetComponent<Kachel>().flow.transform.position, Quaternion.Euler(0, 0, 0));

            next = next.GetComponent<Kachel>().flow;
            transform.position = next.transform.position;
            ende = transform.position;
            start = transform.position;
        }
        else
        {
            ende = next.transform.position;
        }

        index = next.GetComponent<Kachel>().index;

        DestroyImmediate(belegtHolz, true);
        DestroyImmediate(belegtHolzPfeil, true);

        GetFlowFromIndex();
    }

    //Für Gegenstands Animation 
    private void FixedUpdate()
    {
        if (start != ende)
        {
            start = ZugAnimation();
            transform.position = start;

        }
    }

    //Gegenstands Animation
    Vector3 ZugAnimation()
    {
        richtung = ende - start;
        richtung = Vector3.ClampMagnitude(richtung, 0.05f);

        Vector3 newStart;
        newStart = start + richtung;

        return newStart;

    }

}

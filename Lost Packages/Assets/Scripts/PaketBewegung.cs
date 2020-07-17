using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaketBewegung : MonoBehaviour
{
    public int index;
    public GameObject next;
    private GameObject old;

    public GameObject belegtPaket;
    public GameObject belegtPaketPfeil;
    public GameObject belegtPaketTemp, belegtPaketPfeilTemp;

    public Vector3 start;
    public Vector3 ende;
    public Vector3 richtung;

    public GameObject strudelAnimation;
    private GameObject strudel;


    // Start is called before the first frame update
    void Start()
    {
        index = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().paketStartIndex;
        GetFlowFromIndex();
        start = transform.position;
        ende = transform.position;
    }

    //Setzt next auf das Feld in Flussrichtung, wenn es nich von einem Gegenstand belegt ist und setzt das nächste Feld auf package = true
    void GetFlowFromIndex()
    {
        next = GameObject.Find("Kachel " + index);
        old = next;
        next = GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow;
        if (next.GetComponent<Kachel>().clear == false || GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index == next.GetComponent<Kachel>().index)
        {
            next = old;
        }

        next.GetComponent<Kachel>().clear = false;
        next.GetComponent<Kachel>().package = true;

        belegtPaketTemp = Instantiate(belegtPaket, next.transform.position, Quaternion.identity);

        if (next != GameObject.Find("Kachel " + index) && !GameObject.Find("Kachel " + index).GetComponent<Kachel>().strudelBool)
        {
            belegtPaketPfeilTemp = Instantiate(belegtPaketPfeil, GameObject.Find("Kachel " + index).transform.position, Quaternion.Euler(0, 0, (float)GameObject.Find("Kachel " + index).GetComponent<Kachel>().flowAngle));
        }
    }

    //Der Gegenstand wird auf das nächste Feld gesetzt, die alten belegten Kacheln werden entfernt und das GetFlowFromIndex wird erneut aufgerufen
    public void GetToNextField()
    {
        GameObject.Find("Kachel " + next.GetComponent<Kachel>().index).GetComponent<Kachel>().clear = true;
        GameObject.Find("Kachel " + next.GetComponent<Kachel>().index).GetComponent<Kachel>().package = false;

        if (GameObject.Find("Kachel " + index).GetComponent<Kachel>().altFlowBool)
        {
            GameObject.Find("Kachel " + index).GetComponent<Kachel>().ChangePfeil();
        }

        if(next.GetComponent<Kachel>().strudelBool)
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

        DestroyImmediate(belegtPaketTemp, true);
        DestroyImmediate(belegtPaketPfeilTemp, true);

        GetFlowFromIndex();
    }

    //Für Paket Animation 
    private void FixedUpdate()
    {
        if(start != ende)
        {
            start = ZugAnimation();
            transform.position = start;
        }
    }

    //Paket Animation
    Vector3 ZugAnimation()
    {
        richtung = ende - start;
        richtung = Vector3.ClampMagnitude(richtung, 0.05f);

        Vector3 newStart;
        newStart = start + richtung;

        return newStart;   
    }
}

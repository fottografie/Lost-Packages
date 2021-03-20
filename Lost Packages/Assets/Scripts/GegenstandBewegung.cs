using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GegenstandBewegung : MonoBehaviour
{
    public bool paketBool;

    public int index;
    public GameObject next;
    private GameObject current;
    private GameObject old;

    //public GameObject belegtHolzObject;
    //public GameObject belegtHolz;
    //
    //public GameObject belegtHolzPfeilObject;
    //public GameObject belegtHolzPfeil;



    public GameObject belegtKachel;
    public GameObject belegtKachelObject;
    public GameObject belegtPfeil;
    public GameObject belegtPfeilObject;



    //public GameObject belegtPaket;
    //public GameObject belegtPaketPfeil;
    //public GameObject belegtPaketTemp, belegtPaketPfeilTemp;


    public bool fischernetzBool;

    public Vector3 start;
    public Vector3 ende;
    public Vector3 richtung;

    public GameObject strudelAnimation;
    private GameObject strudel;

    public bool coin;
    public GameObject coinExplosion;
    public AudioSource coinSound;


    // Start is called before the first frame update
    void Start()
    {
        if (paketBool)
        {
            index = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().paketStartIndex;
        }
        GetFlowFromIndex();
        old = current;
        start = transform.position;
        ende = transform.position;
    }

    //Setzt next auf das Feld in Flussrichtung, wenn es nicht von einem Gegenstand belegt ist und instantiiert darauf ein belegt-Feld
    void GetFlowFromIndex()
    {
        current = GameObject.Find("Kachel " + index);
        next = GameObject.Find("Kachel " + index).GetComponent<Kachel>().flow;

        if (paketBool)
        {
            if (next.GetComponent<Kachel>().clear == false || GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index == next.GetComponent<Kachel>().index)
            {
                next = current;
            }
        }
        else
        {
            if (!next.GetComponent<Kachel>().clear)
            {
                next = current;
            }

            GameObject.Find("Kachel " + index).GetComponent<Kachel>().clear = true;
            if (coin)
            {
                GameObject.Find("Kachel " + index).GetComponent<Kachel>().package = false;
            }

            if (fischernetzBool)
            {
                GameObject.Find("Kachel " + index).GetComponent<Kachel>().fischernetz = false;
                next.GetComponent<Kachel>().fischernetz = true;
            }
        }

        next.GetComponent<Kachel>().clear = false;

        if (coin || paketBool)
        {
            next.GetComponent<Kachel>().package = true;
        }

        if (paketBool)
        {
            belegtKachel = Instantiate(belegtKachelObject, next.transform.position, Quaternion.identity);

            if (next != GameObject.Find("Kachel " + index) && !GameObject.Find("Kachel " + index).GetComponent<Kachel>().strudelBool)
            {
                belegtPfeil = Instantiate(belegtPfeilObject, GameObject.Find("Kachel " + index).transform.position, Quaternion.Euler(0, 0, (float)GameObject.Find("Kachel " + index).GetComponent<Kachel>().flowAngle));
            }
        }
        else
        {

            if (next != GameObject.Find("Kachel " + index) && !GameObject.Find("Kachel " + index).GetComponent<Kachel>().strudelBool && GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index != index)
            {
                belegtPfeil = Instantiate(belegtPfeilObject, GameObject.Find("Kachel " + index).transform.position, Quaternion.Euler(0, 0, GameObject.Find("Kachel " + index).GetComponent<Kachel>().flowAngle));
            }
            else if (next == GameObject.Find("Kachel " + index))
            {
                belegtKachel = Instantiate(belegtKachelObject, next.transform.position, Quaternion.Euler(0, 0, 0));
                next.GetComponent<Kachel>().clear = false;
            }
        }
    }

    //Der Gegenstand wird auf das nächste Feld gesetzt, die alten belegten Kacheln werden entfernt und GetFlowFromIndex wird erneut aufgerufen
    public void GetToNextField()
    {
        if (paketBool)
        {
            GameObject.Find("Kachel " + next.GetComponent<Kachel>().index).GetComponent<Kachel>().clear = true;
            GameObject.Find("Kachel " + next.GetComponent<Kachel>().index).GetComponent<Kachel>().package = false;
        }

        //GameObject.Find("Kachel " + next.GetComponent<Kachel>().index).GetComponent<Kachel>().clear = true;
        //if (coin)
        //{
        //    GameObject.Find("Kachel " + next.GetComponent<Kachel>().index).GetComponent<Kachel>().package = false;
        //}

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

        DestroyImmediate(belegtKachel, true);
        DestroyImmediate(belegtPfeil, true);

        old = current;
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


    public void CoinExplosion()
    {
        StartCoroutine(WaitToExplode());
        StartCoroutine(WaitToDelete());
    }


    IEnumerator WaitToExplode()
    {
        yield return new WaitForSeconds(0.4f);
        Instantiate(coinExplosion, transform.position, Quaternion.Euler(0, 0, 0));

        FindObjectOfType<AudioManager>().PlayRandomOfKind("CoinSound", 5);
    }

    IEnumerator WaitToDelete()
    {
        yield return new WaitForSeconds(0.6f);
        next.GetComponent<Kachel>().clear = true;
        GameObject.Find("Kachel " + next.GetComponent<Kachel>().index).GetComponent<Kachel>().package = false;

        DestroyImmediate(belegtKachel, true);
        DestroyImmediate(belegtPfeil, true);
        DestroyImmediate(gameObject, true);
        yield return new WaitForSeconds(.1f);
        DestroyImmediate(coinExplosion, true);
    }

    public void StepBack()
    {
        next.GetComponent<Kachel>().clear = false;
        next.GetComponent<Kachel>().package = false;
        current.GetComponent<Kachel>().clear = false;
        current.GetComponent<Kachel>().package = false;

        index = current.GetComponent<Kachel>().index;
        transform.position = current.transform.position;
        start = transform.position;
        ende = transform.position;

        DestroyImmediate(belegtKachel, true);
        DestroyImmediate(belegtPfeil, true);
        
        GetFlowFromIndex();
    }
}

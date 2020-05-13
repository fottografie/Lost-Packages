using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GegenstandBewegung : MonoBehaviour
{
    public int index;
    public GameObject next;
    public GameObject old;

    // Start is called before the first frame update
    void Start()
    {
        GetFlowFromIndex();
    }

    void GetFlowFromIndex()
    {
        old = next;
        GameObject f = GameObject.Find("Kachel " + index);
        next = f.GetComponent<Kachel>().flow;
        if (next.GetComponent<Kachel>().clear == false)
        {
            next = old;
        }
        next.GetComponent<Kachel>().clear = false;
    }


    public void GetToNextField()
    {
        transform.position = next.transform.position;
        index = next.GetComponent<Kachel>().index;
        
        GetFlowFromIndex();
    }

}

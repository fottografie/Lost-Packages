using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaketBewegung : MonoBehaviour
{
    public int startIndex;
    public int index;
    public GameObject next;

    // Start is called before the first frame update
    void Start()
    {
        if(tag == "Holzplanke")
        {
            startIndex = Mathf.RoundToInt(Random.Range(2, 36));
        }

        index = startIndex;
        
        GetFlowFromIndex();
    }

    void GetFlowFromIndex()
    {
        GameObject f = GameObject.Find("Kachel " + index);
        next = f.GetComponent<Kachel>().flow;
    }


    public void GetToNextField()
    {
        transform.position = next.transform.position;
        index = next.GetComponent<Kachel>().index;
        GetFlowFromIndex();
    }

}

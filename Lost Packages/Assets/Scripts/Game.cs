using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject spieler;
    public GameObject paket;
    public GameObject holzplanke;
    public GameObject[] holz;
    public GameObject parent;
    public int spielerIndex;

    // Start is called before the first frame update
    void Start()
    {
        setStartPoint();
    }

    //public void OnMouseDown()
    //{
    //    GameObject next = GameObject.FindGameObjectWithTag("Paket");
    //    next.GetComponent<PaketBewegung>().GetToNextField();
    //
    //    holz = GameObject.FindGameObjectsWithTag("Holzplanke");
    //    foreach(GameObject h in holz)
    //    {
    //        h.GetComponent<PaketBewegung>().GetToNextField();
    //    }
    //
    //}


    void setStartPoint()
    {
        for(int i = 0; i < 3; i++)
        {
            parent = GameObject.Find("Kachel " + Mathf.RoundToInt(Random.Range(2, 36)));
            Instantiate(holzplanke, parent.transform.position, Quaternion.Euler(0, 0, 0));
        }

        parent = GameObject.Find("Kachel 37");
        Instantiate(paket, parent.transform.position, Quaternion.Euler(0, 0, 0));

        parent = GameObject.Find("Kachel " + spielerIndex);
        Instantiate(spieler, parent.transform.position, parent.transform.rotation);
    }

}

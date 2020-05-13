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
        holz = new GameObject[3];
        setStartPoint();
    }


    void setStartPoint()
    {
        for (int i = 0; i < 3; i++)
        {
            int rand = Mathf.RoundToInt(Random.Range(2, 36));
            parent = GameObject.Find("Kachel " + rand);
            holz[i] = Instantiate(holzplanke, parent.transform.position, Quaternion.Euler(0, 0, 0));
            holz[i].GetComponent<GegenstandBewegung>().index = rand;
        }

        parent = GameObject.Find("Kachel 37");
        Instantiate(paket, parent.transform.position, Quaternion.Euler(0, 0, 0));

        parent = GameObject.Find("Kachel " + spielerIndex);
        Instantiate(spieler, parent.transform.position, parent.transform.rotation);
    }

}

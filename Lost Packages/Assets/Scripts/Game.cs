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
    public int spielerStartIndex;
    public int paketStartIndex;
    public int depth;
    public bool finish;
    GameObject paketKachel;
    public List<GameObject> options = new List<GameObject>();
    List<GameObject> newOptions = new List<GameObject>();
    public GameObject[] holzKachel;

    // Start is called before the first frame update
    void Start()
    {
        holz = new GameObject[3];
        holzKachel = new GameObject[holz.Length];
        setStartPoint();
        
        finish = false;
        depth = 1;
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

        parent = GameObject.Find("Kachel " + paketStartIndex);
        Instantiate(paket, parent.transform.position, Quaternion.Euler(0, 0, 0));

        parent = GameObject.Find("Kachel " + spielerStartIndex);
        Instantiate(spieler, parent.transform.position, parent.transform.rotation);


    }


    public void MinimumRounds(GameObject paketKachel_, GameObject[] holzKachel_)
    {
        paketKachel = paketKachel_;
        holzKachel = holzKachel_;
        
        GetAllOptions();
       
        foreach (GameObject o in options)
        {
            if(o == paketKachel)
            {
                Debug.Log("Fertig" + depth);
                finish = true;
            }
        }
        
        while (!finish)
        {
            depth++;

            //for(int i = 0; i < holzKachel.Length; i++)
            //{
            //    Debug.Log(GameObject.Find("Kachel " + holzKachel[i].GetComponent<GegenstandBewegung>().index).GetComponent<Kachel>().flow);
            //    holzKachel[i] = GameObject.Find("Kachel " + holzKachel[i].GetComponent<GegenstandBewegung>().index).GetComponent<Kachel>().flow;
            //}

            MinimumRounds(paketKachel.GetComponent<Kachel>().flow, holzKachel);
        }
    }

    void GetAllOptions()
    {
        GameObject[] PlayerNextField = new GameObject[3];
        if (depth == 1)
        {
            options.Add(GameObject.Find("Kachel " + spielerStartIndex));
        }

        GameObject[] optionsTemp = new GameObject[options.Count]; 

        for(int i = 0; i < options.Count; i++)
        {
            optionsTemp[i] = options[i];
        }

        options.Clear();
        foreach(GameObject o in optionsTemp)
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerNextField = o.GetComponent<Kachel>().PlayerNextField();
                if (o.GetComponent<Kachel>().playerNextField[i] != null)
                {
                    //for(int j = 0; j < holzKachel.Length; j++)
                    //{
                    //    if (o.GetComponent<Kachel>().playerNextField[i] != holzKachel[j])
                    //    {
                            newOptions.Add(o.GetComponent<Kachel>().playerNextField[i]);
                    //    }
                    //}
                }
                
            }
        }
        options = newOptions;
    }

}

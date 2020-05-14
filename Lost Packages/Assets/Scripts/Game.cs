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

    // Start is called before the first frame update
    void Start()
    {
        holz = new GameObject[3];
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


    public void MinimumRounds(GameObject paketKachel_)
    {
        paketKachel = paketKachel_;
        
        
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
            MinimumRounds(paketKachel.GetComponent<Kachel>().flow);
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
                    newOptions.Add(PlayerNextField[i]);
                }
                
            }
        }
        options = newOptions;
    }

}

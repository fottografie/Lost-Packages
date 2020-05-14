using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielerBewegung : MonoBehaviour
{
    public GameObject optionKachel;
    public GameObject[] options = new GameObject[3];
    public GameObject[] felder = new GameObject[3];
    public int index;
    public int minimumRounds;
    public GameObject[] minimumOptions;


    // Start is called before the first frame update
    void Start()
    {
        index = GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().spielerStartIndex;
        options = GameObject.Find("Kachel " + index).GetComponent<Kachel>().PlayerNextField();
        ShowOptions();
        GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().MinimumRounds(GameObject.Find("Kachel " + GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().paketStartIndex).GetComponent<Kachel>().flow);
        minimumRounds = GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().depth;
        minimumOptions = new GameObject[GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().options.Count];
        for(int i = 0; i < minimumOptions.Length; i++)
        {
            minimumOptions[i] = GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().options[i];
        }
    }

    public void ShowOptions()
    {
        options = GameObject.Find("Kachel " + index).GetComponent<Kachel>().PlayerNextField();
        DestroyAllOptional();
        for (int i = 0; i < 3; i++)
        {
            if (options[i] != null && options[i].GetComponent<Kachel>().clear)
            {
                felder[i] = Instantiate(optionKachel, options[i].transform.position, Quaternion.Euler(0, 0, 0));
                felder[i].GetComponent<optionKachel>().index = options[i].GetComponent<Kachel>().index;
            }
        }
    }

    public void DestroyAllOptional()
    {
        GameObject[] optional = GameObject.FindGameObjectsWithTag("optionKachel");
        for (int i = 0; i < optional.Length; i++)
        {
            DestroyImmediate(optional[i], true);
        }
    }


    public GameObject Test()
    {
        Debug.Log(GameObject.Find("Kachel 37").GetComponent<Kachel>().index);
        return GameObject.Find("Kachel " + GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().paketStartIndex).GetComponent<Kachel>().flow;
    }

}
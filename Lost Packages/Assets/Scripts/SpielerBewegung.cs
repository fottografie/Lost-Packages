using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielerBewegung : MonoBehaviour
{
    public GameObject optionKachel;
    public GameObject[] options = new GameObject[3];
    public GameObject[] felder = new GameObject[3];
    public int index;


    // Start is called before the first frame update
    void Start()
    {
        index = GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().spielerIndex;
        options = GameObject.Find("Kachel " + index).GetComponent<Kachel>().PlayerNextField();
        ShowOptions();
    }

    public void ShowOptions()
    {
        options = GameObject.Find("Kachel " + index).GetComponent<Kachel>().PlayerNextField();
        DestroyAllOptional();
        for (int i = 0; i < 3; i++)
        {
            if (options[i] != null)
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

}
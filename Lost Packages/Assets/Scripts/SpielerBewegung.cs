using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielerBewegung : MonoBehaviour
{
    public GameObject optionKachel;
    public GameObject optionPfeil;
    public float angle; 
    public GameObject[] options = new GameObject[3];
    public GameObject[] felder = new GameObject[3];
    public int index;
    public int minimumRounds;
    public GameObject[] minimumOptions;

    public GameObject[] holzKachel;


    // Start is called before the first frame update
    void Start()
    {
        holzKachel = new GameObject[3];

        for(int i = 0; i < holzKachel.Length; i++)
        {
            holzKachel[i] = GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().holz[i];

        }

        index = GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().spielerStartIndex;
        options = GameObject.Find("Kachel " + index).GetComponent<Kachel>().PlayerNextField();
        ShowOptions();


        GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().MinimumRounds(GameObject.Find("Kachel " + GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().paketStartIndex).GetComponent<Kachel>().flow, holzKachel);
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
        angle = GameObject.Find("Kachel " + index).transform.eulerAngles.z;
        DestroyAllOptional();

        //Pfeile
        Instantiate(optionPfeil, transform.position, Quaternion.Euler(0, 0, angle));
        float m = (angle + 60) % 360; 

        if (GameObject.Find("Kachel " + index).GetComponent<Kachel>().GetOption(m) != null && GameObject.Find("Kachel " + index).GetComponent<Kachel>().GetOption(m).GetComponent<Kachel>().clear) {
            Instantiate(optionPfeil, transform.position, Quaternion.Euler(0, 0, m));
        }

        Debug.Log((angle + 300) % 360);
        Debug.Log(GameObject.Find("Kachel " + index).GetComponent<Kachel>().GetOption((angle + 300) % 360));
        if (GameObject.Find("Kachel " + index).GetComponent<Kachel>().GetOption((angle + 300)%360) != null && GameObject.Find("Kachel " + index).GetComponent<Kachel>().GetOption((angle + 300) % 360).GetComponent<Kachel>().clear) {
            Instantiate(optionPfeil, transform.position, Quaternion.Euler(0, 0, (angle + 300) % 360));
        }

        //Kacheln
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


    }
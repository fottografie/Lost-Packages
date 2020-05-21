using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionKachel : MonoBehaviour
{
    public int index;
    public GameObject Kachelbelegt;
    private GameObject[] KachelnBelegt;
    //public GameObject[] kacheln;

    // Start is called before the first frame update
    void Start()
    {
        //  kacheln = GameObject.FindGameObjectsWithTag("Kachel");
        KachelnBelegt = new GameObject[4];
    }

    private void OnMouseDown()
    {
        GameObject[] holz = GameObject.FindGameObjectsWithTag("Holzplanke");
        for (int i = 0; i < holz.Length; i++)
        {
            holz[i].GetComponent<GegenstandBewegung>().GetToNextField();
            GameObject.Find("Kachel " + holz[i].GetComponent<GegenstandBewegung>().index).GetComponent<Kachel>().clear = true;
        }

        GameObject.FindGameObjectWithTag("Spieler").transform.position = transform.position;
        GameObject.FindGameObjectWithTag("Spieler").transform.rotation = GameObject.Find("Kachel " + index).transform.rotation;

        GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().spielerStartIndex = index;
        GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index = index;
        GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().ShowOptions();
        GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().GetToNextField();


        if(GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index == GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().index)
        {
            Debug.Log("GEWONNEN");
            SceneManager.LoadScene("Win", LoadSceneMode.Single);
        }




        KachelnBelegt = GameObject.FindGameObjectsWithTag("KachelBelegt");
        for (int k = 0; k < KachelnBelegt.Length; k++)
        {
            DestroyImmediate(KachelnBelegt[k], true); 
        }
            for (int j = 1; j < 38; j++)
        {
            if (!GameObject.Find("Kachel " + j).GetComponent<Kachel>().clear)
            {
                Instantiate(Kachelbelegt, GameObject.Find("Kachel " + j).GetComponent<Kachel>().transform.position, GameObject.Find("Kachel " + j).GetComponent<Kachel>().transform.rotation);
            } 
        }

    }
}

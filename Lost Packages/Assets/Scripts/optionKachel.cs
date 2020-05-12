using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionKachel : MonoBehaviour
{
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("Spieler").transform.position = transform.position;
        GameObject.FindGameObjectWithTag("Spieler").transform.rotation = GameObject.Find("Kachel " + index).transform.rotation;

        GameObject.FindGameObjectWithTag("Background").GetComponent<Game>().spielerIndex = index;
        GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index = index;
        GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().ShowOptions();
        GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().GetToNextField();

        GameObject[] holz = GameObject.FindGameObjectsWithTag("Holzplanke");

        for(int i = 0; i < holz.Length; i++)
        {
            holz[i].GetComponent<PaketBewegung>().GetToNextField();
        }
    }
}

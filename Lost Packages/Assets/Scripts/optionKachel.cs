using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class optionKachel : MonoBehaviour
{
    public int index;

    //Wenn der Spieler auf eines der options Felder geklickt hat, werden alle Gegenstände, das Paket und der Spieler ein Feld weiter gesetzt
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

        GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().spielerStartIndex = index;
        GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index = index;
        GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().GetToNextField();
        GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().ShowOptions();
        GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().SetZuege(-1);
        if(GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().GetZuege() < 1)
        {
            Debug.Log("VERLOREN!");
            SceneManager.LoadScene("Loose", LoadSceneMode.Single);
        }
        GameObject.FindGameObjectWithTag("Background").GetComponent<GameManager>().ShowZuege();


        //Überprüfung ob der Spieler gewonnen hat (ob er auf dem gleichen Feld wie das Paket steht)
        if (GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index == GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().index)
        {
            Debug.Log("GEWONNEN!");
            SceneManager.LoadScene("Win", LoadSceneMode.Single);
        }

    }
}

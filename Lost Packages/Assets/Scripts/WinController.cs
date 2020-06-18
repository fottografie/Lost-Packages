using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controller der anzeigt, wie viele Züge man benötigt hat
public class WinController : MonoBehaviour
{
    public Text zuegeLabel;
    public int zuege;


    void Start()
    {
        zuege = PlayerPrefs.GetInt("Zuganzahl");
        zuegeLabel.GetComponent<Text>().text = "Du hast " + (PlayerPrefs.GetInt("maxZuege") - zuege) + " Züge benötigt";
    }
}

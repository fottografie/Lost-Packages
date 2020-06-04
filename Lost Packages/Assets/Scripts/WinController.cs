using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    public Text zuegeLabel;
    public int zuege;


    // Start is called before the first frame update
    void Start()
    {
        zuege = PlayerPrefs.GetInt("Zuganzahl");
        zuegeLabel.GetComponent<Text>().text = "Du hast " + (20 - zuege) + " Züge benötigt";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

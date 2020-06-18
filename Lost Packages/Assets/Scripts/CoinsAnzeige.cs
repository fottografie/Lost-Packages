using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsAnzeige : MonoBehaviour
{
    public Text coinsLabel;

    // Start is called before the first frame update
    void Start()
    {
        coinsLabel.GetComponent<Text>().text = "" + PlayerPrefs.GetInt("Coins");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughCoins : MonoBehaviour
{
    public GameObject schlossObject;
    public string link;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Coins") <= GameObject.Find(link).GetComponent<OpenLevel>().minCoins)
        {
            Instantiate(schlossObject, transform.position, transform.rotation);
        }
    }
}

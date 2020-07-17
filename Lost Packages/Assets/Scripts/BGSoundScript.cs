using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Objekt wird in eine neue Scene übernommen (für Soundelemente)
public class BGSoundScript : MonoBehaviour
{
    private static BGSoundScript instance = null;
    public static BGSoundScript Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

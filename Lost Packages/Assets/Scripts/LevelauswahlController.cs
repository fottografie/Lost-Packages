using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelauswahlController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().FadeOutController("BackgroundWaves");
    }
}

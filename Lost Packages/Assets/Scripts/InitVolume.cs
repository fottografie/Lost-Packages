using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InitVolume : MonoBehaviour
{
    public AudioMixer mixer;


    // Start is called before the first frame update
    void Start()
    {
        mixer.SetFloat("MusikVol", Mathf.Log10(PlayerPrefs.GetFloat("MusikFloat")) * 20);
        mixer.SetFloat("SoundeffekteVol", Mathf.Log10(PlayerPrefs.GetFloat("SoundEffectsFloat") * 20));
    }

    public float GetVolume()
    {
        float value;
        bool result = mixer.GetFloat("Musik", out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }
}

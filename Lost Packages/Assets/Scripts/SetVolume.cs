using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public string group;


    private int firstPlay;
    public Slider musikSlider, soundeffectsSlider; 
    private float musikFloat, soundeffectsFloat;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(group, Mathf.Log10(sliderValue) * 20);
    }

    private void Start()
    {
        firstPlay = PlayerPrefs.GetInt("FirstPlay");

        if (firstPlay == 0)
        {
            musikFloat = 0.5f;
            soundeffectsFloat = 0.75f;
            musikSlider.value = musikFloat;
            soundeffectsSlider.value = soundeffectsFloat;
            PlayerPrefs.SetFloat("MusikFloat", musikFloat);
            PlayerPrefs.SetFloat("SoundEffectsFloat", soundeffectsFloat);
            PlayerPrefs.SetInt("FirstPlay", -1);
        }
        else
        {
            musikFloat = PlayerPrefs.GetFloat("MusikFloat");
            musikSlider.value = musikFloat;
            soundeffectsFloat = PlayerPrefs.GetFloat("SoundEffectsFloat");
            soundeffectsSlider.value = soundeffectsFloat;
        }
    }


    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("MusikFloat", musikSlider.value);
        PlayerPrefs.SetFloat("SoundEffectsFloat", soundeffectsSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSoundSettings();
        }
    }
}

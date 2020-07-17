using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Scene Transition
public class LevelLoader : MonoBehaviour
{
    public AudioSource wind;

    public Animator transition;
    public float transitionTime = 2f;

    public void TransitionToNextLevel(string scene)
    {
        FindObjectOfType<AudioManager>().Play("Wind");
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string scene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(scene);
    }

}

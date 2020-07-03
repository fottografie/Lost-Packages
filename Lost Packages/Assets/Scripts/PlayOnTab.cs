using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnTab : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator.StopPlayback();    
    }

    private void OnMouseDown()
    {
        animator.Play("Base Layer.KokusnussAnimation", 0, 0);
    }
}

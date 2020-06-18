using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Element, welches einen Dialog triggert / beginnt
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    //Startet einen den angegeben Dialog
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}

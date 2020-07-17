using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;


    void Start()
    {
        sentences = new Queue<string>();
    }

    //Blendet das Dialogfenster ein und startet den Dialog
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    //Zeigt den nächsten Satz eines Dialogs an
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Lässt jeden Buchstaben einzeln erscheinen
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            FindObjectOfType<AudioManager>().PlayRandomOfKind("KeyboardSound", 9);
            dialogueText.text += letter;
            yield return null;
        }
    }

    //Beendet den Dialog und bledet das Dialogfenster aus
    void EndDialogue()
    {
        if (GameObject.Find("GameManager") != null)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().dialogueActive = false;
        }
        animator.SetBool("IsOpen", false);
    }
}

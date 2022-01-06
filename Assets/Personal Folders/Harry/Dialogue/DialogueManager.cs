using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public float typingSpeed;
    //public Animator animator;

    private Queue<string> sentences;

    void Start () 
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        //animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    void EndDialogue()
    {
        //animator.SetBool("IsOpen", false);
<<<<<<< Updated upstream
        DialogueButton.SetActive(false);
        DialogueBox.SetActive(false);
=======
        FindObjectOfType<TalkingNPC>().StopTalk();
>>>>>>> Stashed changes
    }
}
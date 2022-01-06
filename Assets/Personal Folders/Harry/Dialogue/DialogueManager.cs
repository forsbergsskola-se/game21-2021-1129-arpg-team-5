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
    public float typingSpeed = 0.05f;
    private Queue<string> sentences;

    void Awake() 
    {
        sentences = new Queue<string>();
        typingSpeed = 0.05f;
    }

    public void StartDialogue (Dialogue dialogue)
    {
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
        if (sentences.Count == 1)
        {
            FindObjectOfType<TalkingNPC>().ReplayOption();

        }
        
        else if (sentences.Count == 0)
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
        FindObjectOfType<TalkingNPC>().StopTalk();
    }
}
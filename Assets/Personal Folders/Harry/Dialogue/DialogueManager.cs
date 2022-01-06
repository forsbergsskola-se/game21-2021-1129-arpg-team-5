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
    private TMP_Text button;
    public float typingSpeed { get; private set; } = 0.05f;
    private Queue<string> sentences;

    void Awake() 
    {
        sentences = new Queue<string>();
        typingSpeed = 0.05f;
        button = FindObjectOfType<HUD>().ContinueButton.GetComponentInChildren<TMP_Text>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        button.text = "Continue >>";
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
            button.text = "Leave";
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
    
    public void SlowDialogue()
    {
        typingSpeed = 0.1f;
    }
    public void MediumDialogue()
    {
        typingSpeed = 0.05f;
    }
    public void FastDialogue()
    {
        typingSpeed = 0.03f;
    }
    public void VeryFastDialogue()
    {
        typingSpeed = 0.01f;
    }
}
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
    private Image head;
    public Sprite DialogueHead;

    public float slowTypeSpeed = 0.1f;
    public float mediumTypeSpeed = 0.05f;
    public float fastTypeSpeed = 0.03f;
    public float veryFastTypeSpeed = 0.01f;
    
    public float typingSpeed { get; private set; } = 0.03f;
    private Queue<string> sentences;

    void Awake()
    {
        typingSpeed = veryFastTypeSpeed;
        button = FindObjectOfType<HUD>().ContinueButton.GetComponentInChildren<TMP_Text>();
        head = FindObjectOfType<HUD>().DialogueHeadNPC;
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        dialogueText.text = "";
        head.sprite = DialogueHead;
        nameText.text = dialogue.name;
        button.text = "Continue >>";
        sentences.Clear();
        
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }
    

    public void DisplayNextSentence()
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
        typingSpeed = slowTypeSpeed;
    }
    public void MediumDialogue()
    {
        typingSpeed = mediumTypeSpeed;
    }
    public void FastDialogue()
    {
        typingSpeed = fastTypeSpeed;
    }
    public void VeryFastDialogue()
    {
        typingSpeed = veryFastTypeSpeed;
    }
}
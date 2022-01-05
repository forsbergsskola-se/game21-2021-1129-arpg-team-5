using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private GameObject DialogueBox;
    private GameObject DialogueButton;
    private TMP_Text nameText;
    private TMP_Text dialogueText;
    //public Animator animator;

    private Queue<string> sentences;

    private void Awake()
    {
        throw new NotImplementedException();
    }

    void Start () 
    {
        sentences = new Queue<string>();
        DialogueBox = FindObjectOfType<HUD>().ShopDialogue.gameObject;
        DialogueButton = DialogueBox.transform.Find("Continue Button").gameObject;
        dialogueText = FindObjectOfType<HUD>().ShopDialogue;
        nameText = FindObjectOfType<HUD>().NPCName.GetComponentInChildren<TMP_Text>();
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
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        DialogueButton.SetActive(false);
        DialogueBox.SetActive(false);
    }
}
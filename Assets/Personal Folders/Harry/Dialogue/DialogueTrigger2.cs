using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Team5.Ui;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger2 : MonoBehaviour {
    
    public Dialogue dialogue;
    private Image head;
    private TMP_Text nameText;
    private GameObject dialogueText;
    public Sprite headSprite;
    private Queue<string> sentences;
    private float typingSpeed;
    private TMP_Text button;
    private Button Continue;
    private GameObject RepeatButton;
    private Button Repeat;
    public String Name;



    private void Awake()
    {
        nameText = FindObjectOfType<HUD>().NPCName.GetComponent<TMP_Text>();
        dialogueText = FindObjectOfType<HUD>().ShopDialogue.gameObject;
        Continue = FindObjectOfType<HUD>().ContinueButton.GetComponent<Button>();
        button = FindObjectOfType<HUD>().ContinueButton.GetComponentInChildren<TMP_Text>();
        typingSpeed = FindObjectOfType<DialogueManager>().typingSpeed;
        dialogueText.GetComponent<TMP_Text>().text = "";
        head = FindObjectOfType<HUD>().DialogueHeadNPC;
        sentences = new Queue<string>();
        //Repeat = FindObjectOfType<HUD>().RepeatButton;
        //RepeatButton = Repeat.gameObject;
    }
    


    public void TriggerDialogue()
    {
        head.sprite = this.gameObject.GetComponent<DialogueTrigger>().headSprite;
        nameText.text = Name;
        Debug.Log($"Starting conversation with {Name}: via {this.name}");
        button.text = "Continue >>";
        StartConvo();
    }

    public void Update()
    {
        Continue.onClick.AddListener( () => ParameterOnClick($"{nameof(Continue)} was pressed!"));
    }


    private void StartConvo()
    {
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        NextSentence();
    }
    
    
    private void NextSentence()
    {
        /*if (sentences.Count == 2)
        {
            RepeatButton.SetActive(true);
            Repeat.onClick.AddListener( () => ParameterOnClick2($"{nameof(Repeat)} was pressed!"));
            FindObjectOfType<TalkingNPC>().ReplayOption();
            button.text = "Leave";
        } */

        StopAllCoroutines();
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
        
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
    }
    
    private void EndDialogue()
    {
        FindObjectOfType<TalkingNPC>().StopTalk();
    }
    
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.GetComponent<TMP_Text>().text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.GetComponent<TMP_Text>().text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }
    private void ParameterOnClick(string test)
    {
        NextSentence();
    }
    /*private void ParameterOnClick2(string test)
    {
        RepeatButton.SetActive(false);
        TriggerDialogue();
    }*/
}
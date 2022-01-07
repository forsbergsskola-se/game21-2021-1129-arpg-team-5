using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Entities.Player;
using Team5.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private TMP_Text button;
    //private bool NPCDialogue;
    //private bool KillQuest;
    //private bool CollectQuest;

    //private bool killQuestActive = false;
    //private bool collectQuestActive;
    //private bool reset = false;
    //private int killTarget;
    //private GameObject player;
    private int playerKills;

    public float slowTypeSpeed = 0.1f;
    public float mediumTypeSpeed = 0.05f;
    public float fastTypeSpeed = 0.03f;
    public float veryFastTypeSpeed = 0.01f;



    public float typingSpeed { get; private set; } = 0.03f;
    private Queue<string> sentences;

    void Awake()
    {
        //NPCDialogue = FindObjectOfType<DialogueTrigger>().NPCDialogue;
        //KillQuest = FindObjectOfType<DialogueTrigger>().KillQuest;
        //CollectQuest = FindObjectOfType<DialogueTrigger>().CollectQuest;
        //killTarget = FindObjectOfType<DialogueTrigger>().KillTarget;
        //player = GameObject.FindGameObjectWithTag("Player");
        typingSpeed = fastTypeSpeed;

        
        sentences = new Queue<string>();
        button = FindObjectOfType<HUD>().ContinueButton.GetComponentInChildren<TMP_Text>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        //killQuestActive = false;
        //reset = false;
        
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
        
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        /*if (sentences.Count == 1 && KillQuest == true)
        {
            killQuestActive = true;
            FindObjectOfType<TalkingNPC>().ReplayOption();
            button.text = "Leave";
        }
        
        playerKills = player.GetComponent<PlayerController>().killCount;
        string kills = $"You have killed {playerKills} out of {killTarget}";*/

        StopAllCoroutines();

        /*if (killQuestActive == true)
        {
            StartCoroutine(TypeSentence(kills));
        }*/
        
        string sentence = sentences.Dequeue(); 
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

        /*if (killQuestActive)
        {
            reset = true;
        }*/
    }

    IEnumerator KillDialogue(string sentence)
    {
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
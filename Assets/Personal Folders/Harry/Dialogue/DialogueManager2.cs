using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Entities.Player;
using Team5.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager2 : MonoBehaviour
{
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

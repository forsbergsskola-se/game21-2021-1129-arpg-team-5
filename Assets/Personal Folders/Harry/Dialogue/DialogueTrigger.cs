using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Team5.Ui;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {

    public bool NPCDialogue = false;
    public bool KillQuest = false;
    public bool CollectQuest = false;
    public int KillTarget;
    public int EggTarget;
    public Dialogue dialogue;
    private Image head;

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        head = FindObjectOfType<HUD>().DialogueHeadNPC;
    }
    
    public bool CrowBlack = false;
    public bool CrowWhite = false;
    public bool EnemyGreen = false;
    public bool EnemyPurple = false;
    public bool EnemyRed = false;
    public bool EnemyYellow = false;
    
    public Sprite CrowBlackSprite;
    public Sprite CrowWhiteSprite;
    public Sprite EnemyGreenSprite;
    public Sprite EnemyPurpleSprite;
    public Sprite EnemyRedSprite;
    public Sprite EnemyYellowSprite;
    private GameObject Dialogue;

    private void Awake()
    {
        Dialogue = FindObjectOfType<HUD>().ShopDialogue.gameObject;
    }
    
    // Only checks sprite when Dialogue triggered
    private void Update()
    {
        if (Dialogue.activeInHierarchy)
        {
            AssignSprite();
        }
    }

    // Assigns character dialogue head sprite and disables others
    private void AssignSprite()
    {
        if (CrowBlack == true)
        {
            head.sprite = CrowBlackSprite;
            Disable(true,false,false,false,false,false);
        }
        
        if (CrowWhite == true)
        {
            head.sprite = CrowWhiteSprite;
            Disable(false,true,false,false,false,false);

        }
        
        if (EnemyGreen == true)
        {
            head.sprite = EnemyGreenSprite;
            Disable(false,false,true,false,false,false);

        }
        
        if (EnemyPurple == true)
        {
            head.sprite = EnemyPurpleSprite;
            Disable(false,false,false,true,false,false);

        }
        
        if (EnemyRed == true)
        {
            head.sprite = EnemyRedSprite;
            Disable(false,false,false,false,true,false);

        }
        
        if (EnemyYellow == true)
        {
            head.sprite = EnemyYellowSprite;
            Disable(false,false,false,false,false,true);

        }
    }
    
    // mass enable/ disable method
    private void Disable(bool one, bool two, bool three, bool four, bool five, bool six)
    {
        CrowBlack = one;
        CrowWhite = two;
        EnemyGreen = three;
        EnemyPurple = four;
        EnemyRed = five;
        EnemyYellow = six;
    }
}
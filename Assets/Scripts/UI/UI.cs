using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Combat;
using Team5.Control;
using Team5.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private TMP_Text reviveText;
    private int reviveCount;
    private TMP_Text healthText;
    private float healthCount;
    private TMP_Text killText;
    private float killCount;
    
    private void Update()
    {
        // Keeps track of Revivals on Canvas
        
        reviveCount = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().reviveCounter;
        reviveText = FindObjectOfType<HUD>().ReviveText;
        reviveText.text = "Revivals: " + reviveCount;
        
        healthCount = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().healthPoint;
        healthText = FindObjectOfType<HUD>().HealthText;
        healthText.text = "Health: " + healthCount;
        
        killCount = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>().killCount;
        killText = FindObjectOfType<HUD>().KillCountText;
        killText.text = "Kills: " + killCount;
    }
}

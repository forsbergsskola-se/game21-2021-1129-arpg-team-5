using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Control;
using Team5.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private TMP_Text reviveText;
    private int reviveCount;
    
    private void Update()
    {
        // Keeps track of Revivals on Canvas
        
        reviveCount = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().reviveCounter;
        reviveText = FindObjectOfType<HUD>().ReviveText;
        reviveText.text = "Revivals: " + reviveCount;
    }
}

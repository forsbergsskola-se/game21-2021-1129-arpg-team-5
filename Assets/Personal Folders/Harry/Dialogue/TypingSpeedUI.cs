using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;

public class TypingSpeedUI : MonoBehaviour
{
    private float typingSpeed;
    private float slow = 0.5f;
    private float medium = 0.1f;
    private float fast = 0.05f;
    private float veryFast = 0.01f;

    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;

    private void Update()
    {
        typingSpeed = FindObjectOfType<DialogueManager>().typingSpeed;

        if (typingSpeed == slow)
        {
            OnOff(true, false, false, false);
        }
        
        else if (typingSpeed == medium)
        {
            OnOff(false, true, false, false);
        }
        
        else if (typingSpeed == fast)
        {
            OnOff(false,false,true,false);
        }

        else if (typingSpeed == veryFast)
        {
            OnOff(false,false,false,true);
        }
    }

    public void SlowDialogue()
    {
        typingSpeed = 0.5f;
    }
    public void MediumDialogue()
    {
        typingSpeed = 0.1f;
    }
    public void FastDialogue()
    {
        typingSpeed = 0.05f;
    }
    public void VeryFastDialogue()
    {
        typingSpeed = 0.01f;
    }

    private void OnOff(bool slow, bool medium, bool fast, bool veryFast)
    {
        one.SetActive(slow);
        two.SetActive(medium);
        three.SetActive(fast);
        four.SetActive(veryFast);
    }
    
}

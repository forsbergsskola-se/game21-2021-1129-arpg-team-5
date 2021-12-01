using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using Logic;
using UnityEngine;

public class DoorControll : MonoBehaviour , IInteractable
{
    [SerializeField] private Texture2D lockedCursor;
    [SerializeField] private Texture2D unlockedCursor;

    private Animator Dooropen;
    private GameObject player;
    private bool isOpen;
    private bool isLocked = true;

    private static readonly int IsOpen = Animator.StringToHash("isOpen");
    
    
    void Awake()
    {
        Dooropen = gameObject.GetComponent<Animator>();
        
        
        
        // TEMPORARY FOR TESTING
        StartCoroutine(UnlockDoor());
    }
    

    public Texture2D mouseTexture => isLocked ? lockedCursor : unlockedCursor;


    public void OnHover()
    {
        if (isLocked)
        {
            if (!waitForSound)
            {
                GetComponent<AudioSource>().Play();
                StartCoroutine(WaitForSound());
            }
        }
    }

    public void OnClick(Vector3 mouseClickVector)
    {
        if (isLocked)
            return;
        
        if (!isOpen)
        {
            isOpen = true;
            Dooropen.SetBool(IsOpen, true);
            Debug.Log("does this work");
        }
        else
        {
            isOpen = false;
            Dooropen.SetBool(IsOpen, false);
            Debug.Log("does this work");
        }
        
        
        
        // Check player distance to door.
        
        // If door is close
        // Open door
        
        // If door not close
        
        // Tell stupid player to get closer to door.
        
        // Then if player is close enough to door
        // Open door
        
        // When door is open
        // Tell player to move through the door
        
        // When the player has moved through the door
        // Close the door behind them.
    }
    
    
    
    // TEMPORARY TIMER FOR UNLOCK!
    IEnumerator UnlockDoor()
    {
        yield return new WaitForSeconds(10);
        isLocked = false;
    }


    private bool waitForSound;
    IEnumerator WaitForSound()
    {
        waitForSound = true;
        yield return new WaitForSeconds(1);
        waitForSound = false;
    }
}

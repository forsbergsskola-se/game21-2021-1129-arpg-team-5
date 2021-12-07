using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using Logic;
using Team5.Core;


public class Destructible : Entity, IInteractable
{
    public GameObject destroyedVersion;

    public Texture2D mouseTexture=> cursorTexture;
    
    public Texture2D cursorTexture;
    
    private bool IsDestroyed=true;

    
    // private GameObject player;
    //
    // private void Start()
    // {
    //     player= GameObject.FindWithTag("Player");
    // }
    

    public void OnClick(Vector3 mouseClickVector)
    {
        if (IsDestroyed== true)
        {
            gameObject.SetActive(false);
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            IsDestroyed = false;
        }
        
    }

    public void OnHover()
    {
        
    }
}

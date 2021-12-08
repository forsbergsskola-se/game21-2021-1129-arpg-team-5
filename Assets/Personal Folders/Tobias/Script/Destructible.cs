using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using Logic;
using Team5.Core;


public class Destructible : MonoBehaviour, IInteractable
{
    public GameObject destroyedVersion;

    public Texture2D mouseTexture=> cursorTexture;
    
    public Texture2D cursorTexture;
    
    private bool IsDestroyed=true;
    
    //TODO Public Health to make it accessible to the designers.  
    
    private GameObject player;
    
    //TODO Animations Reference For Explosion. 

    public GameObject explosion;
    //TODO Animations Reference For after the Explosion
    
    private void Start()
    {
        //TODO get the health component
        
        
        //TODO Check for distance between Player and vase.
        
        player= GameObject.FindWithTag("Player");
        Vector3.Distance(player.transform.position, transform.position);
    }

    public void OnClick(Vector3 mouseClickVector)
    {
        //TODO If distance between the object are to big you cant run the animation, You start to run closer to the vase to destroy it.

        //TODO Explosion Animation Run. 
        
        //TODO Idle After the Explosion should run.
        //TODO Explosion if it happened and its on idle after explosion you can walk though ruble.   
        
        gameObject.SetActive(false);
        Instantiate(explosion, transform.position, transform.rotation);
    }

    
    
    public void OnHover()
    {
        
    }
}

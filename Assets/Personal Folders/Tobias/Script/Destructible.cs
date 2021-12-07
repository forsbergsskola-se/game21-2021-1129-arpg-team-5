using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using Logic;


public class Destructible : MonoBehaviour, IInteractable
{
    public GameObject destroyedVersion;

    public Texture2D mouseTexture => cursorTexture;
    public Texture2D cursorTexture;
    public void OnClick(Vector3 mouseClickVector)
    {
        Debug.Log("Onclick on vase");
        gameObject.SetActive(false);
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        
    }

    public void OnHover()
    {
        
    }
}

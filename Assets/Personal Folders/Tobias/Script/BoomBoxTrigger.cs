using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team5.Core;

public class BoomBoxTrigger : MonoBehaviour, IInteractable
{
    public Texture2D cursorTexture;
    public Texture2D mouseTexture=> cursorTexture;
    public void OnHoverEnter()
    {
        
    }

    public void OnHoverExit()
    {
    }

    public void OnClick(Vector3 mouseClickVector)
    {
    }
    
}

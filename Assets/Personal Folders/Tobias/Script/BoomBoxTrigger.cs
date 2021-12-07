using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic;

public class BoomBoxTrigger : MonoBehaviour, IInteractable
{
    public Texture2D cursorTexture;
    public Texture2D mouseTexture=> cursorTexture;
    public void OnHover()
    {
        
    }

    public void OnClick(Vector3 mouseClickVector)
    {
        throw new System.NotImplementedException();
    }
    
}

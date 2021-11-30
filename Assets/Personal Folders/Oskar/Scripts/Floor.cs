using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour, IInteractable
{
    public void OnHover()
    {
        Debug.Log("I hover over the Floor");
    }
    public void OnClick()
    {
        Debug.Log("I clicked on the Floor");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    public void OnHover()
    {
        Debug.Log("I hover over the Box");
    }
    public void OnClick()
    {
        Debug.Log("I clicked on the Box");
    }
}

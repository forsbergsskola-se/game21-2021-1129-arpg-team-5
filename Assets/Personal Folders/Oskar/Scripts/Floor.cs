using Entities.Player;
using Logic;
using UnityEngine;

namespace Personal_Folders.Oskar.Scripts
{
    public class Floor : MonoBehaviour, IInteractable
    {
        public Texture2D mouseTexture => cursorTexture;
        public Texture2D cursorTexture;
        public void OnHover()
        {
            Debug.Log("I hover over the Floor");
        }
        public void OnClick()
        {
            Debug.Log("I clicked on the Floor");
        }
    }
}

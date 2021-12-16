using Team5.Core;
using UnityEngine;

namespace Personal_Folders.Oskar.Scripts
{
    public class Box : MonoBehaviour, IInteractable
    {
        public Texture2D mouseTexture => cursorTexture;
        public Texture2D cursorTexture;
        public void OnHoverEnter()
        {
            Debug.Log("I hover over the Box");
        }

        public void OnHoverExit()
        {
        }

        public void OnClick(Vector3 mouseClickVector)
        {
            Debug.Log("I clicked on the Box");
        }
    }
}

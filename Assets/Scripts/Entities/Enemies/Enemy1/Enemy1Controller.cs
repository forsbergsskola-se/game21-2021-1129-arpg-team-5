using System;
using Control;
using Team5.Core;
using Team5.EntityBase;
using UnityEngine;

namespace Enemies
{
    public class Enemy1Controller : Enemy, IInteractable
    {
        public Texture2D temp;
        
        public Texture2D mouseTexture
        {
            get => temp;
        }
        
        
        public void OnHover()
        {
        }
        public void OnClick(Vector3 mouseClickVector)
        {
        }
    }
}
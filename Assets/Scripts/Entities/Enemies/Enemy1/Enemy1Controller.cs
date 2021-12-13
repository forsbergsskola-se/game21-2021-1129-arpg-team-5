using System;
using Team5.Control;
using Team5.Core;
using Team5.Entities;
using UnityEngine;

namespace Team5.Entities.Enemies.Enemy1
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
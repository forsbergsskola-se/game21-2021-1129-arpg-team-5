using System;
using Control;
using Logic;
using Team5.EntityBase;
using UnityEngine;

namespace Enemies
{
    public class Enemy1Controller : Enemy, IInteractable
    {
        public Texture2D temp;

        private MouseController mouseController;

        private Outline outline;
        [SerializeField]
        private Color OutlineColor = Color.red;
        [SerializeField, Range(0f, 10f)]
        private float OutlineWidth = 5f;
        private bool EnemyIsClicked;
        
        
        public Texture2D mouseTexture
        {
            get => temp;
        }
        
        
        public void OnHover()
        {
        }
       
        
        private void Start()
        {
            outline = gameObject.AddComponent<Outline>();
            outline.enabled = false;
            
            mouseController = FindObjectOfType<MouseController>();
            mouseController.ChangedTarget += ChangedTarget; // This here makes our ChangeTarget method run when the event inside mousecontoller is invoked.
        }

        private void Update()
        {
            outline.OutlineColor = OutlineColor;
            outline.OutlineWidth = OutlineWidth;
        }

       
        void ChangedTarget(object sender, bool temp)
        {
            outline.enabled = false;
            EnemyIsClicked = false;
        }

        
        public void OnClick(Vector3 mouseClickVector)
        {
            outline.enabled = true;
            EnemyIsClicked = true;
        }

        
        public void OnMouseEnter()
        {
            outline.enabled = true;
        }

       
        public void OnMouseExit()
        {
            if(!EnemyIsClicked)
            outline.enabled = false;
        }
    }
}
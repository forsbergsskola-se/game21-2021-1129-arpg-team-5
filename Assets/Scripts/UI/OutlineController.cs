using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Control;
using Team5.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Team5.Ui
{
    public class OutlineController : MonoBehaviour
    {
        private MouseController mouseController;

        private Outline outline;
        [SerializeField] private Outline.Mode outlineMode;
        [SerializeField] private Color OutlineColor = Color.red;
        [SerializeField] private Color AlternateOutlineColor = Color.magenta;
        [SerializeField, Range(0f, 10f)] private float OutlineWidth = 5f;
        public bool IsClicked = false;
        private void Awake()
        {
            // outline = gameObject.AddComponent<Outline>();

            outline = GetComponentInChildren<Outline>();
            if (outline == null) 
                outline = GetComponent<Outline>();
            if (outline == null)
            {
                Debug.Log($"<color=cyan>Failed to find Outline component in {name}. Please make sure there is a OutLine component on the object you want a outline around.</color> <color=red>[Script auto disabled because of error]</color>");
                this.enabled = false;
                return;
            }


            outline.enabled = false;
            
            outline.OutlineMode = outlineMode;
            outline.OutlineColor = OutlineColor;
            outline.OutlineWidth = OutlineWidth;

            mouseController = FindObjectOfType<MouseController>();
            mouseController.ChangedTarget += ChangedTarget; // This here makes our ChangeTarget method run when the event inside mousecontoller is invoked.
        }



        public void UseAlternateColor(bool value)
        {
            if(enabled)
                outline.OutlineColor = value ? AlternateOutlineColor : OutlineColor;
        }
        


        void ChangedTarget(object sender, bool temp)
        {
            if (this.enabled)
            {
                outline.enabled = false;
                IsClicked = false;
            }
        }

        public void OnClick()
        {
            if (this.enabled)
            {
                outline.enabled = true;
                IsClicked = true;
            }
        }


        public void OnMouseEnter()
        {
            if (this.enabled)
            {
                outline.enabled = true;
            }
        }


        public void OnMouseExit()
        {
            if (this.enabled)
            {
                if (!IsClicked)
                    outline.enabled = false;
            }
        }

        public void DisableOutlineController()
        {
            enabled = false;
            outline.enabled = false;
        }

        public void EnableOutLineController()
        {
            enabled = true;
        }
    }
}
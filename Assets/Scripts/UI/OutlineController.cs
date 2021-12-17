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
        [SerializeField]
        private Outline.Mode outlineMode;
        [SerializeField]
        private Color OutlineColor = Color.red;
        [SerializeField, Range(0f, 10f)]
        private float OutlineWidth = 5f;
        public bool IsClicked = false;
        private void Start()
        {
            outline = gameObject.AddComponent<Outline>();
            outline.enabled = false;

            mouseController = FindObjectOfType<MouseController>();
            mouseController.ChangedTarget += ChangedTarget; // This here makes our ChangeTarget method run when the event inside mousecontoller is invoked.
        }

        private void Update()
        {
            outline.OutlineMode = outlineMode;
            outline.OutlineColor = OutlineColor;
            outline.OutlineWidth = OutlineWidth;
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
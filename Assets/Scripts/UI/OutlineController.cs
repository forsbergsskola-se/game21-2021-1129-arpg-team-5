using System.Collections;
using System.Collections.Generic;
using Control;
using Team5.Core;
using Unity.VisualScripting;
using UnityEngine;

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
        public bool EnemyIsClicked = false;
        public Texture2D mouseTexture { get; }
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
            outline.enabled = false;
            EnemyIsClicked = false;
        }

        public void OnClick()
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
            if (!EnemyIsClicked)
                outline.enabled = false;
        }
    }
}
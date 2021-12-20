using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team5.Core;

namespace Team5.Core
{
    public class NameTag : MonoBehaviour, IInteractable
    {
        private GameObject Nametag;
        [SerializeField] private Texture2D Cursor;
        public Texture2D mouseTexture => Cursor;

        public void Awake()
        {
            Nametag = this.gameObject.transform.Find("Name Tag").gameObject;
        }

        public void OnHoverEnter()
        {
            Nametag.SetActive(true);
            Debug.Log($"mouse over {this.name}");
        }
        
        public void OnHoverExit()
        {
            Nametag.SetActive(false);
            Debug.Log($"mouse off {this.name}");
        }

      
        public void OnClick(Vector3 mouseClickVector)
        {
            // click and something happens
        }
    }
}


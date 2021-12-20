using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Team5.Core;
using TMPro;

namespace Team5.Core
{
    public class NameTag : MonoBehaviour, IInteractable
    {
        private GameObject Nametag;
        private TextMeshPro name;
        [SerializeField] private Texture2D Cursor;
        public Texture2D mouseTexture => Cursor;

        public void Awake()
        {
            Nametag = this.gameObject.transform.Find("Name Tag").gameObject;
            name = Nametag.GetComponent<TextMeshPro>();
            name.text = $"{this.gameObject.name}";
        }

        public void OnHoverEnter()
        {
            Nametag.SetActive(true);
            Debug.Log($"mouse hover over {name.text}");
        }
        
        public void OnHoverExit()
        {
            Nametag.SetActive(false);
            Debug.Log($"mouse off {name.text}");
        }

      
        public void OnClick(Vector3 mouseClickVector)
        {
            Nametag.SetActive(false);
        }
    }
}


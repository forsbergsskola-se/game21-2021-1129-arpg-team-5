using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Ui
{
    [RequireComponent(typeof(Outline))]
    public class SimpleOutline : MonoBehaviour
    {
        [SerializeField] private Outline.Mode outlineMode;
        [SerializeField] private Color OutlineColor = Color.red;
        [SerializeField, Range(0f, 10f)] private float OutlineWidth = 5f;

        private void Start()
        {
            var outline = GetComponent<Outline>();
            
            outline.OutlineMode = outlineMode;
            outline.OutlineColor = OutlineColor;
            outline.OutlineWidth = OutlineWidth;
        }
    }
}
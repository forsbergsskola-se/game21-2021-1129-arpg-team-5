using System;
using System.Collections;
using System.Collections.Generic;
using Team5.Entities.Player;
using Team5.Ui;
using TMPro;
using UnityEngine;

namespace Team5.Entities.DestroyableObjects.Rotate
{
    public class Rotate : MonoBehaviour
    {
        private void Update()
        {
            this.transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime * 3);
        }
    }
}


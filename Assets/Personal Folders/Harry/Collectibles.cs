using System.Collections;
using System.Collections.Generic;
using Team5.Entities;
using Team5.Ui;
using TMPro;
using UnityEngine;

namespace Team5.Ui
{
    public class Collectibles : MonoBehaviour
    {
        private TMP_Text EggCounter;
        public int Eggs { get; private set; } = 0;

        void Start()
        {
            EggCounter = FindObjectOfType<HUD>().EggUiText;
        }

        void Update()
        {
            if (Eggs > 0)
            {
                EggCounter.text = "" + Eggs;
            }

            else if (Eggs <= 0)
            {
                EggCounter.text = "0";
            }
        }

        void OnTriggerEnter(Collider other)
        {
            bool pickedUp = false;

            switch (other.gameObject.tag)
            {
                case "Egg":
                    Eggs += 1;
                    pickedUp = true;
                    break;
            }

            if (pickedUp)
            {
                Debug.Log($"{this.name} Picked up: {other.gameObject.tag}");
                other.gameObject.SetActive(false);
            }
        }
    }
}

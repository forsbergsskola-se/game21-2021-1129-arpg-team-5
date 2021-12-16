using System;
using System.Collections;
using System.Net;
using Team5.Combat;
using Team5.Core;
using Team5.Entities;
using Team5.Entities.Player;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Team5.Ui
{
    public class PlayerUI : MonoBehaviour
    {
        private TMP_Text healthText;
        private TMP_Text reviveText;
        private TMP_Text killText;
        private TMP_Text lvlText;
        private TMP_Text scoreText;
        
        private Entity entity;
        private float healthCount;        
        private int reviveCount;
        public int skullCount = 0;

        private Fighter fighter;
        private int killCount;
        public int expLevel = 1;
        public AudioSource sparkle;

        private void Start()
        {
            entity = this.GetComponent<Entity>();
            fighter = this.GetComponent<Fighter>();
            
            healthText = FindObjectOfType<HUD>().HealthText;
            reviveText = FindObjectOfType<HUD>().ReviveText;
            killText = FindObjectOfType<HUD>().KillCountText;
            lvlText = FindObjectOfType<HUD>().LvlText;
            scoreText = FindObjectOfType<HUD>().ScoreText;
        }

        void Update()
        {
            healthText.text = "Health: " + entity.Health;

            reviveCount = this.GetComponent<PlayerController>().reviveCounter;
            reviveText.text = "Revivals: " + reviveCount;
            
            killText.text = "Kills: " + fighter.killCount;

            lvlText.text = "EXP LVL: " + entity.EntityLevel;

            if (skullCount >= 0)
            {
                scoreText.text = "" + skullCount;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            bool pickedUp = false;

            switch (other.gameObject.tag)
            {
                case "WhiteSkull":
                    skullCount += 1;
                    pickedUp = true;
                    break;
                case "RedSkull":
                    skullCount += 5;
                    pickedUp = true;
                    break;
                case "PurpleSkull":
                    skullCount += 10;
                    pickedUp = true;
                    break;
                case "GoldSkull":
                    skullCount += 50;
                    pickedUp = true;
                    break;
            }
            
            if (pickedUp)
            {
                Debug.Log($"{this.name} Picked up: {other.tag}");
                other.gameObject.SetActive(false);
                sparkle.Play();
            }
        }

        // temp values - no magic numbers later
        public void AddSkulls()
        {
            StartCoroutine(Wait(22, 1, 0.2f));
        }
        
        // temp values - no magic numbers later
        public void SubtractSkulls ()
        {
            StartCoroutine(Wait(22, -1, 0.2f));
        }
        
        IEnumerator Wait(int value, int value2, float time )
        {
            for (var i= 0; i<value; i++)
            {
                skullCount += value2;
                yield return new WaitForSeconds(time);
            }
        }
    }
}
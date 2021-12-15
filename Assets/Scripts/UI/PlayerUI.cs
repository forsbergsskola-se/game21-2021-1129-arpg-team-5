using System;
using Team5.Combat;
using Team5.Core;
using Team5.Entities;
using Team5.Entities.Player;
using TMPro;
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

            if (skullCount > 0)
            {
                scoreText.text = "" + skullCount;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("WhiteSkull"))
            {
                skullCount +=1;
            }
            
            else if (other.gameObject.CompareTag("RedSkull"))
            {
                skullCount +=5;
            }
            
            else if (other.gameObject.CompareTag("PurpleSkull"))
            {
                skullCount +=10;
            }
            
            else if (other.gameObject.CompareTag("GoldSkull"))
            {
                skullCount +=50;
            }
            
            Debug.Log($"{this.name} Picked up: {other.tag}");
            other.gameObject.SetActive(false);
            sparkle.Play();
        }

        public void lose22()
        {
            skullCount +=-22;
        }
    }
}
using Team5.Combat;
using Team5.Core;
using Team5.Entities;
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
        
        private Entity entity;
        private float healthCount;        
        private int reviveCount;

        private Fighter fighter;
        private int killCount;
        public int expLevel = 1;

        private void Start()
        {
            entity = this.GetComponent<Entity>();
            fighter = this.GetComponent<Fighter>();
            
            healthText = FindObjectOfType<HUD>().HealthText;
            reviveText = FindObjectOfType<HUD>().ReviveText;
            killText = FindObjectOfType<HUD>().KillCountText;
            lvlText = FindObjectOfType<HUD>().LvlText;
        }

        void Update()
        {
            healthText.text = "Health: " + entity.Health;

            // reviveCount = entity.reviveCounter;
            // reviveText.text = "Revivals: " + reviveCount;
            
            killText.text = "Kills: " + fighter.killCount;

            lvlText.text = "EXP LVL: " + entity.EntityLevel;

            // temp level up logic 

            // if (killCount == 2 && expLevel == 1)
            // {
            //     lvlText.text = "EXP LVL: " + "2";
            //     expLevel++;
            // }
            //
            // if (expLevel == 2)
            // {
            //     entity.maxHealth = 350;
            //     entity.healthPoint = entity.maxHealth;
            // }
        }
    }
}
using Team5.Combat;
using Team5.Core;
using TMPro;
using UI;
using UnityEngine;

namespace UI
{
    public class PlayerUI : MonoBehaviour
    {
        private TMP_Text healthText;
        private TMP_Text reviveText;
        private TMP_Text killText;
        private TMP_Text lvlText;
        
        private Health health;
        private float healthCount;        
        private int reviveCount;

        private Fighter fighter;
        private int killCount;
        public int expLevel = 1;

        private void Start()
        {
            health = this.GetComponent<Health>();
            fighter = this.GetComponent<Fighter>();
            
            healthText = FindObjectOfType<HUD>().HealthText;
            reviveText = FindObjectOfType<HUD>().ReviveText;
            killText = FindObjectOfType<HUD>().KillCountText;
            lvlText = FindObjectOfType<HUD>().LvlText;
        }

        void Update()
        {
            healthCount = health.healthPoint;
            healthText.text = "Health: " + healthCount;

            reviveCount = health.reviveCounter;
            reviveText.text = "Revivals: " + reviveCount;

            killCount = fighter.killCounter;
            killText.text = "Kills: " + killCount;


            // temp level up logic 

            if (killCount == 2 && expLevel == 1)
            {
                lvlText.text = "EXP LVL: " + "2";
                expLevel++;
            }

            if (expLevel == 2)
            {
                health.maxHealth = 350;
                health.healthPoint = health.maxHealth;
            }
        }
    }
}
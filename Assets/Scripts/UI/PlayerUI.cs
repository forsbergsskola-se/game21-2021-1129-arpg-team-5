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
        
        private Health health;
        private float healthCount;        
        private int reviveCount;

        private Fighter fighter;
        private int killCount;

        private void Start()
        {
            health = this.GetComponent<Health>();
            fighter = this.GetComponent<Fighter>();
            
            healthText = FindObjectOfType<HUD>().HealthText;
            reviveText = FindObjectOfType<HUD>().ReviveText;
            killText = FindObjectOfType<HUD>().KillCountText;
        }

        void Update()
        {
            healthCount = health.healthPoint;
            healthText.text = "Health: " + healthCount;

            reviveCount = health.reviveCounter;
            reviveText.text = "Revivals: " + reviveCount;
            
            killCount = fighter.killCounter;
            killText.text = "Kills: " + killCount;
        }
    }
}
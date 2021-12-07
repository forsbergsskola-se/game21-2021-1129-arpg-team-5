using Team5.Combat;
using Team5.Core;
using TMPro;
using UI;
using UnityEngine;

namespace UI
{
    public class PlayerUI : MonoBehaviour
    {
        private TMP_Text reviveText;
        private int reviveCount;
    
        private TMP_Text healthText;
        private Health playerHealth;
        private float healthCount;
    
        private TMP_Text killText;
        private int killCount;
        private Fighter kills;

        private void Start()
        {
            playerHealth = this.GetComponent<Health>();
            kills = this.GetComponent<Fighter>();
            
            reviveText = FindObjectOfType<HUD>().ReviveText;
            healthText = FindObjectOfType<HUD>().HealthText;
            killText = FindObjectOfType<HUD>().KillCountText;
        }

        void Update()
        {
            healthCount = playerHealth.healthPoint;
            healthText.text = "Health: " + healthCount;

            killCount = kills.killCounter;
            killText.text = "Kills: " + killCount;
            
            reviveCount = playerHealth.reviveCounter;
            reviveText.text = "Revivals: " + reviveCount;
        }
    }
}
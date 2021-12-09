using Team5.Combat;
using Team5.EntityBase;
using TMPro;
using UnityEngine;


namespace Team5.Ui
{
    public class UI : MonoBehaviour
    {
        private TMP_Text reviveText;
        private int reviveCount;
        private TMP_Text healthText;
        private float healthCount;
        private TMP_Text killText;
        private float killCount;
    
        private void Update()
        {
            // Keeps track of Revivals on Canvas
        
            // reviveCount = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>().reviveCounter;
            // reviveText = FindObjectOfType<HUD>().ReviveText;
            // reviveText.text = "Revivals: " + reviveCount;
        
            // healthCount = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>().Health;
            // healthText = FindObjectOfType<HUD>().HealthText;
            // healthText.text = "Health: " + healthCount;
            //
            // killCount = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>().killCount;
            // killText = FindObjectOfType<HUD>().KillCountText;
            // killText.text = "Kills: " + killCount;
        }
    }
}
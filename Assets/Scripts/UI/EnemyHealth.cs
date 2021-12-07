using Team5.Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EnemyHealth : MonoBehaviour
    {
        private Health healthStats;
        private float health;
        private TMP_Text healthText;

        private void Start()
        {
            healthStats = this.GetComponent<Health>();
            healthText = this.GetComponentInChildren<TMP_Text>();
        }


        void Update()
        {
            health = healthStats.healthPoint;
            healthText.text = "" + health;
        }
    }
}

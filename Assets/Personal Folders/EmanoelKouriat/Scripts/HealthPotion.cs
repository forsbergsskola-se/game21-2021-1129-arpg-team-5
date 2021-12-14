using UnityEngine;
using Team5.Entities.Player;

namespace team5.Inventory.Items.Consumables
{
    public class HealthPotion : MonoBehaviour, IConsumable
    {
        private PlayerController player;
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public void Consume()
        {
            if (player.Health != player.MaxHealth)
                player.AddHealth(50);
            else
                Debug.Log("Health is already full!");
        }
    }
}
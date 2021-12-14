using UnityEngine;
using Team5.Entities.Player;

namespace team5.Inventory.Items.Consumables
{
    public class HealthPotion : MonoBehaviour, IConsumable
    {
        private PlayerController player;
        [SerializeField] private int healValue;
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public void Consume()
        {
            if (player.Health < player.MaxHealth)
            {
                player.AddHealth(healValue);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Health is already full!");
            }
        }
    }
}
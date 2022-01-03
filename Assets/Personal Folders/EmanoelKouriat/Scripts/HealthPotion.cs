using Team5.Entities;
using UnityEngine;
using Team5.Entities.Player;

namespace Team5.Inventories.Items.Consumables
{
    public class HealthPotion : MonoBehaviour, IConsumable
    {
        private PlayerController player;
        [SerializeField] private int healValue;
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public bool Consume(GameObject user)
        {
            Entity entity = user.GetComponent<Entity>();
            // ##############################################################################
            // TODO: This was needed since the script "forgot" the playercontroller somehow?
            // Why is that?
            // ##############################################################################
            
            

            if (entity.Health < entity.MaxHealth)
            {
                entity.AddHealth(healValue);
                // Destroy(gameObject);
                Debug.Log($"Healed {user} for {healValue} health.");
                return true;
            }
            
            Debug.Log("Health is already full!");
            return false;
        }
    }
}
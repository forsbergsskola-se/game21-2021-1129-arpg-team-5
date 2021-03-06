using Team5.Entities;
using UnityEngine;
using Team5.Entities.Player;

namespace Team5.Inventories.Items.Consumables
{
    public class HealthPotion : MonoBehaviour, IConsumable
    {
        [SerializeField] private int healValue;

        public bool Consume(GameObject user)
        {
            Entity entity = user.GetComponent<Entity>();
            // ##############################################################################
            // TODO: This was needed since the script "forget" the playercontroller somehow?
            // Why is that?
            // ##############################################################################
            
            

            if (entity.Health < entity.MaxHealth)
            {
                entity.ModifyHealth(healValue);
                // Destroy(gameObject);
                Debug.Log($"Healed {user} for {healValue} health.");
                return true;
            }
            
            Debug.Log("Health is already full!");
            return false;
        }
    }
}
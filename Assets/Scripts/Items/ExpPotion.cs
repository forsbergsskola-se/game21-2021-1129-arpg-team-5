using Team5.Entities;
using UnityEngine;

namespace Team5.Inventories.Items.Consumables
{
    public class ExpPotion : MonoBehaviour, IConsumable
    {
        [SerializeField] private int LevelsToAdd;
        
        public bool Consume(GameObject user)
        {
            user.GetComponent<Entity>().EntityLevel += LevelsToAdd;

            return true;
        }
    }
}
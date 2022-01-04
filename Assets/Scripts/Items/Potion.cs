using System.Collections;
using Team5.Entities;
using UnityEngine;

namespace Team5.Inventories.Items.Consumables
{
    public class Potion : MonoBehaviour, IConsumable
    {
        [SerializeField] private float MaxHealthBoost;
        [SerializeField] private float MovementSpeedBoost;
        [SerializeField] private float ArmorBoost;
        
        
        public bool Consume(GameObject user)
        {
            StartCoroutine(potionDuration(30, user));
            
            // user.GetComponent<Entity>().

            return true;
        }

        private IEnumerator potionDuration(float time, GameObject user)
        {
            AddPotionEffect(user);
            yield return new WaitForSeconds(time);
            RemovePotionEffect(user);
        }

        private void AddPotionEffect(GameObject user)
        {
            user.GetComponent<Entity>().ModifyStats(MovementSpeedBoost,MaxHealthBoost,ArmorBoost,1);
        }

        private void RemovePotionEffect(GameObject user)
        {
            user.GetComponent<Entity>().ModifyStats(MovementSpeedBoost,MaxHealthBoost,ArmorBoost,-1);
        }
    }
}
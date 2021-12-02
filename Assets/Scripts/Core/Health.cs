using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        float healthPoint = 100f;
        bool isDead = false;
        public bool IsDead() 
        {
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            healthPoint = Mathf.Max(healthPoint - damage, 0);
            if(healthPoint <= 0)
            {
                Death();
            }
            Debug.Log($"{this.name}'s current health: {this.healthPoint}");
        }

        public void Death()
        {
            if (isDead)
            {
                return;
            }
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}

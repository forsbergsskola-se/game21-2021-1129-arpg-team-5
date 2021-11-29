using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        float health = 100f;
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            Debug.Log(health);
        }
    }
}

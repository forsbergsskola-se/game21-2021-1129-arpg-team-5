using System.Collections;
using System.Collections.Generic;
using Team5.Combat;
using UnityEngine;

namespace Team5.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        float chaseDistance = 5f;
        Fighter fighter;
        GameObject player;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");

        }
        private void Update()
        {
            if (InAttackRange() && fighter.CanAttack(player))
            {
                Debug.Log("Attack!");
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool InAttackRange()
        {
            float distanceWithPlayer = Vector3.Distance(player.transform.position, transform.position);

            return distanceWithPlayer < chaseDistance;

        }

    }
}


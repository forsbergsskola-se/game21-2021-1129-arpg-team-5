using System.Collections;
using System.Collections.Generic;
using Team5.Combat;
using Team5.Core;
using Team5.Movement;
using UnityEngine;

namespace Team5.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        float chaseDistance = 5f;
        Fighter fighter;
        Move move;
        Health health;
        GameObject player;

        Vector3 gaurdLocation;

        private void Start()
        {
            move = GetComponent<Move>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");

            gaurdLocation = transform.position;
        }
        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange() && fighter.CanAttack(player))
            {
                Debug.Log("Attack!");
                fighter.Attack(player);
            }
            else
            {
                move.StartMoveAction(gaurdLocation);
            }
           
        }

        private bool InAttackRange()
        {
            float distanceWithPlayer = Vector3.Distance(player.transform.position, transform.position);

            return distanceWithPlayer < chaseDistance;

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }
}


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
        [SerializeField]
        float suspicionTime = 3f;
        [SerializeField]
        PatrolPath patrolPath;
        [SerializeField]
        float waypointTolerence = 1f;
        Fighter fighter;
        Move move;
        Health health;
        GameObject player;

        Vector3 gaurdLocation;
        float timeScinseLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;

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
                timeScinseLastSawPlayer = 0;
                Debug.Log("Attack!");
                AttackBehaviour();
            }
            else if (timeScinseLastSawPlayer < suspicionTime)
            {
                SuspiciousBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            timeScinseLastSawPlayer += Time.deltaTime;
           
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPos = gaurdLocation;
            if(patrolPath != null)
            {
                if (InWaypoint())
                {
                    FollowWaypoint();
                }
                nextPos = GetCurrentWaypoint();
            }
            move.StartMoveAction(nextPos);
        }
        private bool InWaypoint()
        {
            float distnaceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distnaceToWaypoint < waypointTolerence;
        }

        private void FollowWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWayPoint(currentWaypointIndex);
        }
        private void SuspiciousBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
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


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
        [SerializeField]
        float waypointWaitingTime = 1f;
        Fighter fighter;
        Move move;
        Health health;
        GameObject player;

        Vector3 gaurdLocation;
        float timeScinseLastSawPlayer = Mathf.Infinity;
        float timeScinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;
        private GameObject enemyIndicator;
        private GameObject enemyIndicator2;


        private void Start()
        {
            move = GetComponent<Move>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            gaurdLocation = transform.position;

            if (this.gameObject != player)
            {
                enemyIndicator = this.gameObject.transform.Find("Enemy Indicator").gameObject;
                enemyIndicator2 = this.gameObject.transform.Find("Enemy Indicator2").gameObject;
            }
        }
        private void Update()
        {
            if (health.IsDead())
            {
                enemyIndicator2.SetActive(false);
                return;
            }

            if (InAttackRange() && fighter.CanAttack(player))
            {
                
                Debug.Log($"{this.name}: Attack!");
                AttackBehaviour();
            }
            else if (timeScinseLastSawPlayer < suspicionTime)
            {
                SuspiciousBehaviour();
                if (enemyIndicator == enabled)
                {
                    enemyIndicator.SetActive(false);
                }
            }
            else
            {
                PatrolBehaviour();
                if (enemyIndicator2 == enabled)
                {
                    enemyIndicator2.SetActive(false);
                }

            }
            UpdateTimers();

        }

        private void UpdateTimers()
        {
            timeScinseLastSawPlayer += Time.deltaTime;
            timeScinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPos = gaurdLocation;
            if(patrolPath != null)
            {
                if (InWaypoint())
                {
                    timeScinceArrivedAtWaypoint = 0;
                    FollowWaypoint();
                }
                nextPos = GetCurrentWaypoint();
            }
            if(timeScinceArrivedAtWaypoint > waypointWaitingTime)
            {
                move.StartMoveAction(nextPos);
                
            }
            
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
            timeScinseLastSawPlayer = 0;
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


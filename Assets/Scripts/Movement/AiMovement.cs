using System;
using System.Collections;
using Team5.Combat;
using Team5.Control;
using Team5.Core;
using Team5.Entities;
using TMPro;
using UnityEngine;


namespace Team5.Movement
{
    [RequireComponent(typeof(Entity))]
    public class AiMovement : MonoBehaviour, IAction
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float waypointWaitingTime = 1f;


        private Entity entity;
        private Fighter fighter;
        private Move move;
        private GameObject player;

        // Ui stuff
        private GameObject enemyIndicator;
        private GameObject enemyIndicator2;
        private TMP_Text healthText;

        private int currentWaypointIndex = 0;
        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer;
        private float timeSinceArrivedAtWaypoint;

        private void Start()
        {
            entity = GetComponent<Entity>();
            fighter = GetComponent<Fighter>();
            move = GetComponent<Move>();
            player = GameObject.FindWithTag("Player");

            enemyIndicator = this.gameObject.transform.Find("Enemy Indicator").gameObject;
            enemyIndicator2 = this.gameObject.transform.Find("Enemy Indicator2").gameObject;
            healthText = this.GetComponentInChildren<TMP_Text>();
            guardPosition = transform.position;
        }

        private void Update()
        {
            if (entity.IsDead)
            {
                // disables children
                enemyIndicator2.SetActive(false);
                healthText.enabled = false;
            }

            if (CheckAttackRange() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
                
                if (enemyIndicator == enabled)
                    enemyIndicator.SetActive(false);
            }
            else
            {
                PatrolBehaviour();
                if (enemyIndicator2 == enabled)
                    enemyIndicator2.SetActive(false);
            }
            UpdateTimer();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
            timeSinceLastSawPlayer = 0;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPos = guardPosition;

            if (patrolPath != null)
            {
                if (WaypointReached())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }

                nextPos = GetWaypoint();
            }
            if(timeSinceArrivedAtWaypoint > waypointWaitingTime)
                move.StartMoveAction(nextPos);
        }

        private bool CheckAttackRange()
        {
            return Vector3.Distance(player.transform.position, transform.position) <= chaseDistance;
        }

        private bool WaypointReached()
        {
            return Vector3.Distance(transform.position, GetWaypoint()) <= waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetWaypoint()
        {
            return patrolPath.GetWayPoint(currentWaypointIndex);
        }

        void UpdateTimer()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        // TODO: Do stuffs
        public void Cancel()
        {
            
        }
    }
}


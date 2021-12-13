using System;
using System.Collections;
using Team5.Combat;
using Team5.Control;
using Team5.Core;
using Team5.EntityBase;
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
        
        // death cloud
        // TODO: MOVE 
        public SkinnedMeshRenderer mesh;
        public float dustSpawnTime;
        public float corpseStayTime;
        private ParticleSystem deathCloud;
        
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
            deathCloud = transform.Find("Dust Cloud").GetComponent<ParticleSystem>();

            
            enemyIndicator = this.gameObject.transform.Find("Enemy Indicator").gameObject;
            enemyIndicator2 = this.gameObject.transform.Find("Enemy Indicator2").gameObject;
            healthText = this.GetComponentInChildren<TMP_Text>();
            
            
            guardPosition = transform.position;
        }

        private void Update()
        {

            if (entity.IsDead)
            {
                enemyIndicator2.SetActive(false);
                healthText.enabled = false;
                
                // TODO: MOVE - Starts death cloud
                StartCoroutine(WaitToDisable());
                return;
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
        
        
        // TODO: MOVE
        private IEnumerator WaitToDisable()
        {
            Debug.Log($"Destroy {this.name} in 10 seconds");

            // Dust cloud spawns
            yield return new WaitForSeconds(dustSpawnTime);
            deathCloud.gameObject.SetActive(true);
            deathCloud.Play();
            deathCloud.transform.position = this.gameObject.transform.position;
            
            // Enemy mesh is disabled
            yield return new WaitForSeconds(corpseStayTime);
            mesh.GetComponent<Renderer>().enabled = false;

            // Enemy game object fully disabled
            yield return new WaitForSeconds(10);
            this.gameObject.SetActive(false);
            deathCloud.gameObject.SetActive(false);
        }
    }
}


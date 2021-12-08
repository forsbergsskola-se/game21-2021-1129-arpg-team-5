using System.Collections;
using Team5.Combat;
using Team5.Core;
using Team5.Movement;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Team5.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float waypointWaitingTime = 1f;
        
        private Fighter fighter;
        private Move move;
        private Health health;
        private GameObject player;
        public SkinnedMeshRenderer mesh;
        public float dustSpawnTime;
        public float corpseStayTime;
        public GameObject dustPrefab;

        private Vector3 guardLocation;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int currentWaypointIndex = 0;
        private GameObject enemyIndicator;
        private GameObject enemyIndicator2;
        private EnemyHealth enemyHealth;
        private TMP_Text healthText;

        private void Start()
        {
            move = GetComponent<Move>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            guardLocation = transform.position;

            if (this.gameObject == player) return;
            
            enemyIndicator = this.gameObject.transform.Find("Enemy Indicator").gameObject;
            enemyIndicator2 = this.gameObject.transform.Find("Enemy Indicator2").gameObject;
            enemyHealth = this.GetComponent<EnemyHealth>();
            healthText = enemyHealth.healthTextIndicator;

        }
        private void Update()
        {
            if (health.IsDead())
            {
                enemyIndicator2.SetActive(false);
                healthText.enabled = false;
                
                // enables dust cloud and disables mesh & game object
                StartCoroutine(WaitToDisable());
                return;
            }

            if (InAttackRange() && fighter.CanAttack(player))
            {
                Debug.Log($"{this.name}: Attack!");
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspiciousBehaviour();

                if (enemyIndicator == enabled)
                    enemyIndicator.SetActive(false);
            }
            else
            {
                PatrolBehaviour();
                if (enemyIndicator2 == enabled)
                    enemyIndicator2.SetActive(false);
            }
            
            UpdateTimers();
        }
        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }
        private void PatrolBehaviour()
        {
            Vector3 nextPos = guardLocation;
            
            if(patrolPath != null)
            {
                if (InWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    FollowWaypoint();
                }
                
                nextPos = GetCurrentWaypoint();
            }
            
            if(timeSinceArrivedAtWaypoint > waypointWaitingTime)
                move.StartMoveAction(nextPos);
        }
        private bool InWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
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
            timeSinceLastSawPlayer = 0;
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
        
        private IEnumerator WaitToDisable()
        {
            Debug.Log($"Destroy {this.name} in 10 seconds");

            // Dust cloud spawns
            yield return new WaitForSeconds(dustSpawnTime);
            dustPrefab.SetActive(true);
            dustPrefab.transform.position = this.gameObject.transform.position;
            
            // Enemy mesh is disabled
            yield return new WaitForSeconds(corpseStayTime);
            Debug.Log($"Destroyed {this.name} at timestamp : " + Time.time);
            mesh.GetComponent<Renderer>().enabled = false;

            // Enemy game object fully disabled
            yield return new WaitForSeconds(10);
            this.gameObject.SetActive(false);
        }
    }
}


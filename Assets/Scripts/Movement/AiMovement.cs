using Team5.Combat;
using Team5.Control;
using Team5.Core;
using Team5.EntityBase;
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

        private int currentWaypointIndex = 0;
        private float timeSinceLastSawPlayer;
        private Vector3 guardPosition;

        private void Start()
        {
            entity = GetComponent<Entity>();
            fighter = GetComponent<Fighter>();
            move = GetComponent<Move>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (entity.IsDead)
                return;
            
            if (CheckAttackRange() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            else
            {
                PatrolBehaviour();
            }
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPos = guardPosition;

            if (patrolPath != null)
            {
                if (WaypointReached())
                {
                    CycleWaypoint();
                }

                nextPos = GetWaypoint();
            }

            Debug.Log("Temp");
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





        // TODO: Do stuffs
        public void Cancel()
        {
            
        }
    }
}


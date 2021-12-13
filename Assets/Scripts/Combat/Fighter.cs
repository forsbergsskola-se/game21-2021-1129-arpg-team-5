using Team5.Movement;
using Team5.Core;
using Team5.Entities;
using Team5.Ui;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team5.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [HideInInspector] public int killCount;
        
        [SerializeField] private float baseAccuracyPercentage;
        [SerializeField] private float baseCriticalChance;
        [SerializeField] private float criticalDamageMultiplier;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 1f;

        private float accuracyPercentage;
        private float criticalChance;
        private float timeSinceLastAttack = Mathf.Infinity;

        private GameObject player;
        private GameObject enemyIndicator;
        private Entity target;
        
        private static readonly int Attack1 = Animator.StringToHash("attack");
        private static readonly int StopAttack1 = Animator.StringToHash("stopAttack");

        
        
        public float CriticalChance
        {
            get => criticalChance;
            set => criticalChance = Mathf.Min(value, 100);
        }
        
        public float AccuracyPercentage
        {
            get => accuracyPercentage;
            set => accuracyPercentage = Mathf.Min(value, 100);
        }
        
        
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player");

            AccuracyPercentage = baseAccuracyPercentage;
            CriticalChance = baseCriticalChance;
        }

        
        
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
            {
                return;
            }
            
            if (!GetIsInRange())
            {
                GetComponent<Move>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Move>().Cancel();
                AttackBehaviour();
            }
            
            if (target.IsDead)
            {
                // Disables enemy indicator if enemy dies
                if (this.gameObject != player )
                {
                    EnemyIndicatorInactive();
                }

                // Disables enemy indicator if player dies
                else if (this.gameObject == player )
                {
                    EnemyIndicatorInactiveTarget();
                }
                return;
            }
            
            
            // Activates Enemy Indicator if Player targets enemy
            if (target.gameObject != player)
            {
                EnemyIndicatorActiveTarget();
            }

            // Activates Enemy Indicator if Enemy targets players
            if (target.gameObject == player)
            {
                EnemyIndicatorActive();
            }
            
            // need to add logic for small enemy indicator to go away
        }

        
        
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                if (!target.IsDead)
                {
                    TriggerAttack();
                    timeSinceLastAttack = 0;
                }
            }
        }

        
        
        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger(StopAttack1);
            GetComponent<Animator>().SetTrigger(Attack1);//triggering Hit() from animation
        }
 
        
        
        // Animation Event 
        void Hit()
        {
            if (target == null || target.IsDead) return;
            // Debug.Log($"{this.name} new attack");
            
            // random values for critical hit and accuracy between 0 and 9
            // critChance = 11 - Random.Range(1, 11);
            // accuracyChance = 11 - Random.Range(1, 11);

            // attack with critical hit if lower than critPercent value
            if (Random.Range(0, 100) < CriticalChance)
            {
                var totalAttackValue = weaponDamage * criticalDamageMultiplier;
                Debug.Log($"{this.name} can land critical hit");
                // hit accuracy higher than chance, can attack with critical hit
                if (Random.Range(0, 100) < accuracyPercentage)
                {
                    //Debug.Log($"{this.name}'s {accuracyPercent}% accuracy > {accuracyChance}0% chance");
                    Debug.Log($"{this.name} landed a CRITICAL HIT of {totalAttackValue} on {target.name}!!!");
                    target.TakeDamage(totalAttackValue);
                }

                //  misses attack due to low accuracy
                else
                {
                    //Debug.Log($"{this.name}'s {accuracyPercent}% accuracy < {accuracyChance}0% chance");
                    Debug.Log($"{this.name}'s critical hit missed {target.name}!");
                }
            }
            // attack without critical hit
            else
            {
                if (Random.Range(0, 100) < accuracyPercentage)
                {
                    //Debug.Log($"{this.name}'s {accuracyPercent}% accuracy > {accuracyChance}0% chance");
                    Debug.Log($"{this.name} dealt {weaponDamage} damage to {target.name}");
                    target.TakeDamage(weaponDamage);
                }
                
                //  misses attack due to low accuracy
                else

                {
                    //Debug.Log($"{this.name}'s {accuracyPercent}0% accuracy < {accuracyChance}0% chance");
                    Debug.Log($"{this.name}'s attack missed {target.name}!");
                }
            }

            // Print death
            if (target.IsDead)
            {
                // Debug.Log($"{target.name}'s current health: {target.Health}");
                // Debug.Log($"{target.name} was defeated by {this.name} at {Time.time}");

                if (this.gameObject == player)
                {
                    killCount++;
                }
            }
        }

        
        
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        
        
        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null)
            {
                return false;
            }
            Entity targetToTest = combatTarget.GetComponent<Entity>();
            return targetToTest != null && !targetToTest.IsDead;
        }

        
        
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Entity>();
        }

        
        
        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        
        
        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger(Attack1);
            GetComponent<Animator>().SetTrigger(StopAttack1);
        }

        
        
        //Set enemy indicator active
        public void EnemyIndicatorActive()
        {
            enemyIndicator = this.transform.Find("Enemy Indicator2").gameObject;
            enemyIndicator.SetActive(true); 
        }
        
        
        
        public void EnemyIndicatorActiveTarget()
        {
            enemyIndicator = target.transform.Find("Enemy Indicator").gameObject;
            enemyIndicator.SetActive(true); 
        }
        
        
        
        //Set enemy indicator inactive
        public void EnemyIndicatorInactive()
        {
            if (this.gameObject != player)
            {
                enemyIndicator = this.transform.Find("Enemy Indicator2").gameObject;
                enemyIndicator.SetActive(false);
            } 
        }
        
        
        
        public void EnemyIndicatorInactiveTarget()
        {
            {
                enemyIndicator = target.transform.Find("Enemy Indicator").gameObject;
                enemyIndicator.SetActive(false); 
            }
        }
    }
}

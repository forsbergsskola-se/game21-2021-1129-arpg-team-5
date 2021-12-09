using System.Collections;
using Team5.Core;
using UnityEngine;

namespace Team5.EntityBase
{
    public abstract class Entity : MonoBehaviour
    {
        [HideInInspector] public bool IsDead;
        [HideInInspector] public float MovementSpeed;

        [SerializeField] private int BaseMaxHealth;
        [SerializeField] private int BaseArmor;
        [SerializeField] private float BaseMovementSpeed;
        [SerializeField] private float BaseDamageCooldown;
        [SerializeField] private float EntityLevelValueMuliplier;

        protected bool takeDamageOnCooldown;

        private float health;
        private float armor;
        private float damageCooldownTime;
        private bool isAlive;

        protected float damageResistance;
        protected float maxHealth;
        protected float level;
        
        
        
        /// <summary>
        /// Override this to set gameobjects and similar in child classes.. Then call base when you want the values to be set.
        /// </summary>
        protected virtual void Awake()
        {
            maxHealth = BaseMaxHealth;
            Health = maxHealth;
            Armor = BaseArmor;
            MovementSpeed = BaseMovementSpeed;
            damageCooldownTime = BaseDamageCooldown;
        }

        /// <summary>
        /// Reset the entity to its base statistics. Does not include it's level.
        /// </summary>
        public virtual void ResetEntity()
        {
            maxHealth = BaseMaxHealth;
            Health = maxHealth;
            Armor = BaseArmor;
            MovementSpeed = BaseMovementSpeed;
            damageCooldownTime = BaseDamageCooldown;
        }

        /// <summary>
        /// Sets or gets the entity's health. Will set it to inactive if the health reaches 0.
        /// </summary>
        public virtual float Health
        {
            get => health;
            protected set
            {
                health = value;
                if (health <= 0)
                {
                    OnDeath();
                }
            }
        }

        public virtual float Armor
        {
            get => armor;
            set => damageResistance = (100 - value) / 100;
        }
        
        /// <summary>
        /// Function called on death of a entity. Override to add additional changes on death.
        /// </summary>
        public virtual void OnDeath()
        {
            if (IsDead)
                return;
            IsDead = true;

            Debug.Log($"{name} is now dead!");
            
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        /// <summary>
        /// Get: the level, Set: the Level and scale stats accordingly. 
        /// </summary>
        public virtual float EntityLevel
        {
            get => level;
            set
            {
                var oldMultiplier = EntityLevelValueMuliplier * value;
            
                var bonusMaxHealth = maxHealth - BaseMaxHealth * oldMultiplier;
                var bonusArmor = Armor - BaseArmor * oldMultiplier;
                var bonusMovementSpeed = MovementSpeed - BaseMovementSpeed * oldMultiplier;


                var multiplier = EntityLevelValueMuliplier * value;
            
                maxHealth = BaseMaxHealth * multiplier + bonusMaxHealth;
                Armor = BaseArmor * multiplier + bonusArmor;
                MovementSpeed = BaseMovementSpeed * multiplier + bonusMovementSpeed;

                level = value;
            }
        }
        
        /// <summary>
        /// Deal damage to the Entity if it is not on cooldown and used in other Entity for example Player, GameObject and Enemies.
        /// </summary>
        /// <param name="damageTaken">Damage you deal to Entity</param>
        public virtual void TakeDamage(float damageTaken)
        {
            if (takeDamageOnCooldown) return;
        
            StartCoroutine(DamageCooldown());
            Debug.Log(name + " took " + damageTaken*damageResistance + " damage!");
            Health -= damageTaken * damageResistance;
        }

        private IEnumerator DamageCooldown()
        {
            takeDamageOnCooldown = true;
            yield return new WaitForSeconds(damageCooldownTime);
            takeDamageOnCooldown = false;
        }
    }

}


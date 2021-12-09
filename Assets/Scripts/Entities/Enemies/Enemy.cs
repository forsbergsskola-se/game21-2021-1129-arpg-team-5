using TMPro;
using UnityEngine;

namespace Team5.EntityBase
{
    public abstract class Enemy : Entity
    {
        // TODO: Update this with designer choice. Maybe a healthbar instead, or something else.
        private TMP_Text healthText; 
        
        public override float Health
        {
            get => base.Health;
            protected set
            {
                Debug.Log("Hello I took damage.");
                healthText.SetText(value.ToString());
                base.Health = value;
            }
        }

        protected override void Awake()
        {
            Debug.Log("Works!!!");
            healthText = GetComponentInChildren<TMP_Text>();
            base.Awake();
        }

        // {
        //     Debug.Log("Works!!!");
        //     healthText = GetComponentInChildren<TMP_Text>();
        // }
    }
}


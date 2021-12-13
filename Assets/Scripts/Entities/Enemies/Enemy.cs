using System.Collections;
using Team5.Ui;
using TMPro;
using UnityEngine;

namespace Team5.Entities.Enemies
{
    public abstract class Enemy : Entity
    {
        // TODO: Update this with designer choice. Maybe a healthbar instead, or something else.
        private TMP_Text healthText;
        private TMP_Text hurtText;
        private ParticleSystem blood;
        public float damageHealthDecay = 0.5f;

        
        public override float Health
        {
            get => base.Health;
            protected set
            {
                float temp = (base.Health - value);
                base.Health = value;
                hurtText.SetText(temp.ToString());
                healthText.SetText(Health.ToString());
                
                blood.gameObject.SetActive(true);
                blood.Play();
                hurtText.enabled = true;
                StartCoroutine(WaitAndDisable());
            }
        }
        
        protected override void Awake()
        {
            healthText = GetComponentInChildren<TMP_Text>();
            hurtText = transform.Find("Hurt Health Value (TMP)").GetComponent<TMP_Text>();
            blood = transform.Find("Blood").GetComponent<ParticleSystem>();

            base.Awake();
        }

        protected override void OnDeath()
        {
            //gameObject.GetComponent<OutlineController>().DisableOutlineController();

            if (gameObject.TryGetComponent(out OutlineController outlineController))
                outlineController.DisableOutlineController();
            
            base.OnDeath();
        }
        
        IEnumerator WaitAndDisable()
        {
            yield return new WaitForSeconds(damageHealthDecay);
            hurtText.enabled = false;
            blood.Stop();
            blood.gameObject.SetActive(false);
        }
    }
}


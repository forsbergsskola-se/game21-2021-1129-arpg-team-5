using UnityEngine;
using Team5.Core;
using Team5.Entities;

namespace Team5.Entities.Objects.DestroyableObject
{
    public class DestroyableObjectController: Entity, IInteractable
    {
        public Texture2D mouseTexture=> cursorTexture;
        public Texture2D cursorTexture;
        public GameObject explosion;
        public ParticleSystem SmokeSystem;
        private GameObject player;
        public GameObject Nametag;
        


        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            Vector3.Distance(player.transform.position, transform.position);
        }
        
        protected override void OnDeath()
        {
            gameObject.SetActive(false);
            Instantiate(SmokeSystem, transform.position, transform.rotation);
            Instantiate(explosion, transform.position, transform.rotation);
            IsDead = true;
            
            Nametag.SetActive(false);
            Debug.Log($"mouse over {this.name}");
        }
        
        public void OnHoverExit()
        {
            /*Nametag.SetActive(false);
            Debug.Log($"mouse over {this.name}");*/
        }

        public void OnClick(Vector3 mouseClickVector)
        {
            
        }
        
        public void OnHoverEnter()
        {
            /*Nametag.SetActive(true);
            Debug.Log($"mouse on {this.name}");*/
        }
    }
}
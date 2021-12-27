using System.Collections;
using Team5.Control;
using Team5.Core;
using Team5.Movement;
using Team5.Ui;
using UnityEngine;

namespace Team5.World.Interactables
{
    public class Switch : MonoBehaviour, IInteractable
    {
        public InteractableBarrierController barrier;
        
        [SerializeField] private Texture2D OnHoverMousetexture;
        [SerializeField] private float ActivationDistance;

        private Vector3 playerMoveTarget;
        private MouseController mouseController;
        private GameObject player;
        private IEnumerator goToAndActivate;
        
        
        private void Start()
        {
            if (barrier == null)
            {
                Debug.Log("<color=cyan>Switch has no assigned InteractableBarrierController!</color>  <color=red>[ Script destroyed! ]</color>");
                Destroy(this);
            }
            else
            {
                barrier.IsLocked = true;
                playerMoveTarget = transform.Find("PlayerMoveTarget").position;
                
                mouseController = FindObjectOfType<MouseController>();
                mouseController.ChangedTarget += ChangedTarget; // This here makes our ChangeTarget method run when the event inside mousecontoller is invoked.

                goToAndActivate = GoToAndActivate();
                
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }



        private IEnumerator GoToAndActivate()
        {
            player.GetComponent<Move>().StartMoveAction(playerMoveTarget);
            
            while (true)
            {
                yield return new WaitForSeconds(0.25f);

                if (Vector3.Distance(player.transform.position, playerMoveTarget) < ActivationDistance)
                {
                    ActivateSwitch();
                    break;
                }
            }
        }

        
        
        void ActivateSwitch()
        {
            transform.Find("Lever").transform.Rotate(Vector3.back, 60);
            
            barrier.IsLocked = false;
            GetComponent<BoxCollider>().enabled = false;
            enabled = false;
        }
        
        

        public Texture2D mouseTexture => OnHoverMousetexture;
        public void OnHoverEnter()
        {
            if (player.GetComponent<Move>().TargetReachable(playerMoveTarget))
                GetComponent<OutlineController>().enabled = true;
            else
                GetComponent<OutlineController>().DisableOutlineController();
        }

        public void OnHoverExit()
        {
        }

        public void OnClick(Vector3 mouseClickVector)
        {
            if (!enabled)
                return;
            
            if (player.GetComponent<Move>().TargetReachable(playerMoveTarget))
            {
                StartCoroutine(goToAndActivate);
            }
        }
        
        // Event subscriber that will stop the system for reaching the door if the player clicks something else.
        void ChangedTarget(object sender, bool temp)
        {
            StopCoroutine(goToAndActivate);
        }
    }
}

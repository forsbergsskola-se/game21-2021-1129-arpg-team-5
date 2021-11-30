using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using Team5.Movement;
using UnityEngine;
using Team5.Combat;
using UnityEngine.AI;


namespace Team5.Control
{
    public class PlayerController : Entity

    {
    void Start()
    {
        GetComponent<NavMeshAgent>().speed = MovementSpeed;
    }

    void Update()
    {
        if (InteractWithCombat())
        {
            return;
        }

        if (InteractWithMovement())
        {
            return;
        }

    }

    private bool InteractWithCombat()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits)
        {
            CombatTarget target = hit.transform.GetComponent<CombatTarget>();
             if (!GetComponent<Fighter>().CanAttack(target))
             {
                 continue;
             }

            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Fighter>().Attack(target);
            }

            return true;
        }

        return false;
    }

    private bool InteractWithMovement()
    {
        /*RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hitInfo);
        if (hasHit)
        {
            if (Input.GetMouseButton(0))
            {
                GetComponent<Move>().StartMoveAction(hitInfo.point);
            }

            return true;
        }*/

        return false;
    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    }
}

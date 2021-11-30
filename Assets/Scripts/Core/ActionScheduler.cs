using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team5.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        public void StartAction(IAction action)
        {
            if (currentAction == action) 
                return;

            if(currentAction != null)
            {
                currentAction.Cancel();
            }
            
            currentAction = action;
        }
    }

}
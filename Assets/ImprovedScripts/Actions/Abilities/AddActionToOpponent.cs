using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class AddActionToOpponent : Ability
    {
        System.Type actionName;
        object[] actionParameters;

        public delegate object[] GetParameters();
        GetParameters getParameters;
        public AddActionToOpponent(System.Type actionName, object[] actionParameters)
        {
            this.actionName = actionName;
            this.actionParameters = actionParameters;
            getParameters = DefaultGetParameters;
        }

        public AddActionToOpponent(System.Type actionName, GetParameters getParameters)
        {
            this.actionName = actionName;
            this.getParameters = getParameters;
        }

        public AddActionToOpponent(AddActionToOpponent template)
        {
            actionName = template.actionName;
            actionParameters = template.actionParameters;
            getParameters = template.getParameters;
        }

        public override void PerformGameAction()
        {
            MainSequenceManager.Instance.AddActionToEnemySequence(actionName.ToString(), getParameters());
            OnEnd();
        }

        public object[] DefaultGetParameters()
        {
            return actionParameters;
        }
    }
}

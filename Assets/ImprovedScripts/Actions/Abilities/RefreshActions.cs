using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class RefreshActions : TargetedAbility
    {
        public RefreshActions()
        {
            
        }

        public RefreshActions(RefreshActions template)
        {
            target = template.target;
        }

        public override void PerformGameAction()
        {
            UnitController unit = target.GetComponent<UnitController>();
            unit.SetAttackActionState(UnitController.ActionState.CanAct);
            unit.SetMoveActionState(UnitController.ActionState.CanAct);
            unit.actedThisTurn = false;
            OnEnd();
        }
    }
}

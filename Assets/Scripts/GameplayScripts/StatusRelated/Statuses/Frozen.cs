using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : Status
{

    protected override void Awake()
    {
        vfxPath = "Frozen";
        base.Awake();

        if (card.isMine)
        {
            card.OnAttackStateChanged += FreezeUnit;
            card.OnMoveStateChanged += FreezeUnit;

            //card.SetAttackActionState(UnitController.ActionState.Acted);
            //card.SetMoveActionState(UnitController.ActionState.Acted);
            card.attackState = UnitController.ActionState.Acted;
            card.moveState = UnitController.ActionState.Acted;
        }
    }

    public override void PerformStatus()
    {
        //card.SetAttackActionState(UnitController.ActionState.Acted);
        //card.SetMoveActionState(UnitController.ActionState.Acted);
        card.attackState = UnitController.ActionState.Acted;
        card.moveState = UnitController.ActionState.Acted;
    }

    public void FreezeUnit(UnitController.ActionState state)
    {
        if(state == UnitController.ActionState.CanAct)
        {
            PerformStatus();
        }
    }

    public override void RemoveStatus()
    {
        base.RemoveStatus();

        if (card.isMine)
        {
            card.OnMoveStateChanged -= FreezeUnit;
            card.OnAttackStateChanged -= FreezeUnit;

            if (!card.actedThisTurn)
            {
                // Debug.Log("Allowing to act");
                card.SetAttackActionState(UnitController.ActionState.CanAct);
                card.SetMoveActionState(UnitController.ActionState.CanAct);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshActions : TargetedAbility
{
    public override IEnumerator ActionCoroutine()
    {
        UnitController unit = target.GetComponent<UnitController>();
        unit.SetAttackActionState(UnitController.ActionState.CanAct);
        unit.SetMoveActionState(UnitController.ActionState.CanAct);
        unit.actedThisTurn = false;
        OnEnd();
        yield return null;
    }
}

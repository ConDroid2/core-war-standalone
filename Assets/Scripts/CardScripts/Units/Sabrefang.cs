using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabrefang : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnter = new OnEnterPlay(unit);
        conditions.Add(onEnter);

        SequenceSystem.RefreshActions refreshActions = new SequenceSystem.RefreshActions();
        refreshActions.SetTarget(gameObject);
        SequenceSystem.AddToAttackStack addToAttackStack = new SequenceSystem.AddToAttackStack(unit, refreshActions);

        onEnter.AddAbility(addToAttackStack);
    }
}

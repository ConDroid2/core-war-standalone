using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostshroudTitan : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();

        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.Target target = new SequenceSystem.Target(unit, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit, statusFilter: "Rooted");
        SequenceSystem.DestroyCard destroy = new SequenceSystem.DestroyCard();
        target.AddAbility(destroy);

        SequenceSystem.AddToAdvanceStack advance = new SequenceSystem.AddToAdvanceStack(unit, target);

        onEnterPlay.AddAbility(advance);
        onEnterPlay.AddAbility(target);
    }
}

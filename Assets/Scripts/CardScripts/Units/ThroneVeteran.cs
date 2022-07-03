using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneVeteran : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnter = new OnEnterPlay(unit);
        conditions.Add(onEnter);

        SequenceSystem.TargetAllUnitsInZone target = new SequenceSystem.TargetAllUnitsInZone(2, CardSelector.ZoneFilter.EnemyZones);
        SequenceSystem.KnockbackUnit knockback = new SequenceSystem.KnockbackUnit();
        target.AddAbility(knockback);
        onEnter.AddAbility(target);
    }
}

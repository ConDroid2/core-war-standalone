using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomSylkan : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnTurnEnd onTurnEnd = new OnTurnEnd(unit);
        conditions.Add(onTurnEnd);

        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("1", unit));

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(unit, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);
        targetAll.abilities.Add(damage);

        onTurnEnd.AddAbility(targetAll);
    }
}

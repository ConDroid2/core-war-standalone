using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithSylkan : CardScript
{

    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnTurnStart onStart = new OnTurnStart(GetComponent<InPlayCardController>());
        conditions.Add(onStart);

        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("1", unit));

        SequenceSystem.TargetRandom targetRandom = new SequenceSystem.TargetRandom(unit, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);
        targetRandom.AddAbility(damage);

        onStart.AddAbility(targetRandom);  
    }
}

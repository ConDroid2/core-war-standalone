using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterJahrrach : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnter = new OnEnterPlay(unit);
        conditions.Add(onEnter);

        SequenceSystem.AttackAbility attack = new SequenceSystem.AttackAbility(unit);
        SequenceSystem.Target target = new SequenceSystem.Target(unit, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);
        target.optional = true;
        target.AddAbility(attack);
        onEnter.AddAbility(target);
    }
}

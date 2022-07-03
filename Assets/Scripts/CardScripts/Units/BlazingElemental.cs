using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazingElemental : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.Target target = new SequenceSystem.Target(GetComponent<CardParent>(), zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);

        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("2", unit));
        target.abilities.Add(damage);

        onEnterPlay.AddAbility(target);
    }
}

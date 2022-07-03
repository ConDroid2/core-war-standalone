using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthshakerTitan : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("MyStrength", unit));
        SequenceSystem.Target target = new SequenceSystem.Target(unit, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);
        target.abilities.Add(damage);

        onEnterPlay.AddAbility(target);
        unit.AdvanceStack.Add(target);
    }

    public int GetEarthshakerDamage()
    {
        return GetComponent<UnitController>().cardData.currentStrength;
    }
}

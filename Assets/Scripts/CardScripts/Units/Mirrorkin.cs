using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirrorkin : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.Target target = new SequenceSystem.Target(unit, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);

        SequenceSystem.TargetedTransformation transformation = new SequenceSystem.TargetedTransformation(unit);
        target.abilities.Add(transformation);
        onEnterPlay.AddAbility(target);
    }
}

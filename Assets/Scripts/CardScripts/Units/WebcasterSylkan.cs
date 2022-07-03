using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcasterSylkan : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.AddStatus addStatus = new SequenceSystem.AddStatus("Rooted", 1);

        SequenceSystem.Target targetAbility = new SequenceSystem.Target(unit, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);
        targetAbility.abilities.Add(addStatus);

        onEnterPlay.AddAbility(targetAbility);
    }
}

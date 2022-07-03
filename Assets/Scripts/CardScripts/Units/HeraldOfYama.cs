using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeraldOfYama : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();

        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(unit, zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit);

        SequenceSystem.RemoveStatuses removeStatus = new SequenceSystem.RemoveStatuses();
        targetAll.abilities.Add(removeStatus);
        onEnterPlay.AddAbility(targetAll);

        unit.cardData.statusImmunities.Add("Frozen");
        unit.cardData.statusImmunities.Add("Rooted");
    }
}

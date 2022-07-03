using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EternalDevourer : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        SequenceSystem.DestroyCard destroy = new SequenceSystem.DestroyCard();

        SequenceSystem.Target target = new SequenceSystem.Target(unit, zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit);
        target.abilities.Add(destroy);

        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);
        onEnterPlay.AddAbility(target);
    }
}

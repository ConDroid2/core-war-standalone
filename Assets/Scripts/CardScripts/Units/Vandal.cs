using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vandal : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();

        OnEnterPlay onEnter = new OnEnterPlay(unit);
        conditions.Add(onEnter);

        SequenceSystem.Target target = new SequenceSystem.Target(unit, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Support);
        SequenceSystem.DestroyCard destroyCard = new SequenceSystem.DestroyCard();
        target.AddAbility(destroyCard);

        onEnter.AddAbility(target);
    }
}

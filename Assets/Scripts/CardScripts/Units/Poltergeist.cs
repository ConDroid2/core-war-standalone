using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poltergeist : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.TargetRandom target = new SequenceSystem.TargetRandom(unit, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Support);
        SequenceSystem.DestroyCard destroy = new SequenceSystem.DestroyCard();
        target.AddAbility(destroy);

        onEnterPlay.AddAbility(target);
        unit.AdvanceStack.Add(target);
    }
}

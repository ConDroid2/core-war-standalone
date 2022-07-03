using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bladesmith : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.TargetRandom target =
            new SequenceSystem.TargetRandom(
                unit,
                CardSelector.HandFilter.MyHand,
                typeFilter: CardSelector.TypeFilter.Unit,
                subtypeFilter: "Warrior",
                amount: 2);

        SequenceSystem.BuffInHand buff = new SequenceSystem.BuffInHand(1, 1);
        target.AddAbility(buff);

        onEnterPlay.AddAbility(target);
    }
}

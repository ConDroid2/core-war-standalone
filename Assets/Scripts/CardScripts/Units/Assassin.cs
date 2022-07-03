using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : CardScript
{
    public override void InPlaySetUp()
    {
        OnEnterPlay onPlay = new OnEnterPlay(GetComponent<InPlayCardController>());
        conditions.Add(onPlay);

        SequenceSystem.Target chooseTarget = new SequenceSystem.Target(GetComponent<CardParent>(), zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit, costFilter: 4, costCompare: CardSelector.CostCompare.LessThanOrEqualTo);
        SequenceSystem.DestroyCard destroy = new SequenceSystem.DestroyCard();

        chooseTarget.abilities.Add(destroy);

        onPlay.AddAbility(chooseTarget);
    }
}

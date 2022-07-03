using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshesToAshes : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(card, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit, costFilter: 2, costCompare: CardSelector.CostCompare.LessThanOrEqualTo);
        SequenceSystem.DestroyCard destroy = new SequenceSystem.DestroyCard();
        targetAll.AddAbility(destroy);

        onPlay.AddAbility(targetAll);
    }
}

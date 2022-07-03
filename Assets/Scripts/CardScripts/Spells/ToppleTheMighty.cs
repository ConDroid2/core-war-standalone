using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppleTheMighty : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(card,
            zoneFilter: CardSelector.ZoneFilter.All,
            typeFilter: CardSelector.TypeFilter.All,
            costFilter: 5,
            costCompare: CardSelector.CostCompare.GreaterThanOrEqualTo);
        SequenceSystem.DestroyCard destroy = new SequenceSystem.DestroyCard();
        targetAll.AddAbility(destroy);
        

        onPlay.AddAbility(targetAll);
    }
}

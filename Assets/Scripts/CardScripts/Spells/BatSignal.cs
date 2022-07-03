using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSignal : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.DrawRandomCards draw =
            new SequenceSystem.DrawRandomCards(
                2, typeFilter: CardSelector.TypeFilter.Unit, keywordFilter: "Legendary");

        onPlay.AddAbility(draw);
    }
}

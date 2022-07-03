using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientWritings : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        SequenceSystem.DrawRandomCards draw = new SequenceSystem.DrawRandomCards(amount: 2, typeFilter: CardSelector.TypeFilter.Spell);
        onPlay.AddAbility(draw);
    }
}

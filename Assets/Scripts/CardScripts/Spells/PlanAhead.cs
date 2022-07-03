using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanAhead : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(2);

        SequenceSystem.Target target = new SequenceSystem.Target(card, CardSelector.HandFilter.MyHand);
        SequenceSystem.DiscardCard discardCard = new SequenceSystem.DiscardCard();
        target.abilities.Add(discardCard);

        onPlay.AddAbility(draw);
        onPlay.AddAbility(target);
    }
}

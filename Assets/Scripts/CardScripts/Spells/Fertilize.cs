using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fertilize : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.IncreaseLevel increase = new SequenceSystem.IncreaseLevel(1);

        SequenceSystem.Target target = new SequenceSystem.Target(card, CardSelector.HandFilter.MyHand);

        SequenceSystem.DiscardCard discard = new SequenceSystem.DiscardCard();
        target.abilities.Add(discard);

        onPlay.AddAbility(increase);
        onPlay.AddAbility(target);  
    }
}

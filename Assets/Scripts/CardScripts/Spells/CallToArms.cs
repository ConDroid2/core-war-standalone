using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallToArms : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.TargetMultiple target = new SequenceSystem.TargetMultiple(card, CardSelector.HandFilter.MyHand, subtypeFilter: "Warrior");
        target.SetAmountOfTargets(4);
        target.SetTargetMode(SequenceSystem.TargetMultiple.TargetMode.UpTo);

        SequenceSystem.PlaySelectedCard playCard = new SequenceSystem.PlaySelectedCard();

        target.AddAbility(playCard);

        onPlay.AddAbility(target);
    }
}

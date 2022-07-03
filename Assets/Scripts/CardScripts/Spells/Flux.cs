using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flux : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.TargetMultiple target = new SequenceSystem.TargetMultiple(card, CardSelector.HandFilter.MyHand);
        target.SetAmountOfTargets(100);
        target.SetTargetMode(SequenceSystem.TargetMultiple.TargetMode.UpTo);

        SequenceSystem.ShuffleCardIntoDeck shuffle = new SequenceSystem.ShuffleCardIntoDeck();
        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);

        target.AddAbility(shuffle);
        target.AddAbility(draw);

        onPlay.AddAbility(target);
    }
}

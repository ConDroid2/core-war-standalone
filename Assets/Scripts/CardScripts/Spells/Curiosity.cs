using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curiosity : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();

        Ignite ignite = new Ignite(card, 2);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(3);

        SequenceSystem.TargetMultiple target = new SequenceSystem.TargetMultiple(card, CardSelector.HandFilter.MyHand);
        target.SetAmountOfTargets(2);
        target.SetTargetMode(SequenceSystem.TargetMultiple.TargetMode.ExactAmount);

        SequenceSystem.TargetMultiple igniteTarget = new SequenceSystem.TargetMultiple(card, CardSelector.HandFilter.MyHand);
        igniteTarget.SetAmountOfTargets(1);
        igniteTarget.SetTargetMode(SequenceSystem.TargetMultiple.TargetMode.ExactAmount);

        SequenceSystem.ShuffleCardIntoDeck shuffleCard = new SequenceSystem.ShuffleCardIntoDeck();
        target.abilities.Add(shuffleCard);
        igniteTarget.AddAbility(shuffleCard);

        ignite.ignitedAbility.AddAbility(draw);
        ignite.ignitedAbility.AddAbility(igniteTarget);
        ignite.baseAbility.AddAbility(draw);
        ignite.baseAbility.AddAbility(target);

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosTheory : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        Ignite ignite = new Ignite(card, 2);

        SequenceSystem.TargetRandom random = new SequenceSystem.TargetRandom(card, CardSelector.HandFilter.MyHand, typeFilter: CardSelector.TypeFilter.Spell);
        SequenceSystem.Target target = new SequenceSystem.Target(card, CardSelector.HandFilter.MyHand, typeFilter: CardSelector.TypeFilter.Spell);

        SequenceSystem.PlaySelectedCard playCard = new SequenceSystem.PlaySelectedCard();
        random.AddAbility(playCard);
        target.AddAbility(playCard);

        ignite.baseAbility.AddAbility(random);
        ignite.ignitedAbility.AddAbility(target);
    }
}

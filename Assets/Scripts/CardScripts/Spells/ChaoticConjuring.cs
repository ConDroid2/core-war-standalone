using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaoticConjuring : CardScript
{
    public override void InHandSetUp()
    {
        CardController controller = GetComponent<CardController>();
        Ignite ignite = new Ignite(controller, 3);

        SequenceSystem.PlayRandomCardsFromDeck basePlayRandom = new SequenceSystem.PlayRandomCardsFromDeck(
            typeFilter: CardSelector.TypeFilter.Unit, costFilter: 2, 
            costCompare: CardSelector.CostCompare.LessThanOrEqualTo, 
            otherChecks: (card) => { return !card.keywords.Contains("Legendary"); }, 
            cardsToPlay: 2
        );

        SequenceSystem.PlayRandomCardsFromDeck ignitePlayRandom = new SequenceSystem.PlayRandomCardsFromDeck(
            typeFilter: CardSelector.TypeFilter.Unit, costFilter: 4,
            costCompare: CardSelector.CostCompare.GreaterThanOrEqualTo,
            otherChecks: (card) => { return !card.keywords.Contains("Legendary"); },
            cardsToPlay: 2
        );

        ignite.baseAbility.AddAbility(basePlayRandom);
        ignite.ignitedAbility.AddAbility(ignitePlayRandom);
    }
}

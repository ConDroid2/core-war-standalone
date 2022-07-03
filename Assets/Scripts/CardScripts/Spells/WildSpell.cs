using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildSpell : CardScript
{
    public override void InHandSetUp()
    {
        Ignite ignite = new Ignite(GetComponent<CardController>(), 1);

        SequenceSystem.PlayRandomCardsFromDeck basePlayCards = new SequenceSystem.PlayRandomCardsFromDeck(
            typeFilter: CardSelector.TypeFilter.Spell, 
            costFilter: 4, 
            costCompare: CardSelector.CostCompare.LessThanOrEqualTo);

        SequenceSystem.PlayRandomCardsFromDeck ignitePlayCards = new SequenceSystem.PlayRandomCardsFromDeck(
            typeFilter: CardSelector.TypeFilter.Spell, 
            costFilter: 6, 
            costCompare: CardSelector.CostCompare.LessThanOrEqualTo);

        ignite.baseAbility.AddAbility(basePlayCards);
        ignite.ignitedAbility.AddAbility(ignitePlayCards);
    }
}

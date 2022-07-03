using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchmageOfTheStorm : CardScript
{
    public override void InPlaySetUp()
    {
        OnEnterPlay onEnterPlay = new OnEnterPlay(GetComponent<InPlayCardController>());
        conditions.Add(onEnterPlay);

        SequenceSystem.PlayRandomCardsFromDeck playCards = new SequenceSystem.PlayRandomCardsFromDeck(typeFilter: CardSelector.TypeFilter.Spell, costFilter: 4, costCompare: CardSelector.CostCompare.GreaterThanOrEqualTo, cardsToPlay: 2);

        onEnterPlay.AddAbility(playCards);
    }
}

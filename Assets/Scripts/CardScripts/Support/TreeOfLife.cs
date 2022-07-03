using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOfLife : CardScript
{
    public override void InPlaySetUp()
    {
        SupportController support = GetComponent<SupportController>();

        OnTurnStart turnStart = new OnTurnStart(support);
        conditions.Add(turnStart);

        SequenceSystem.PlayRandomCardsFromDeck playRandomCard =
            new SequenceSystem.PlayRandomCardsFromDeck(
                typeFilter: CardSelector.TypeFilter.Unit);
        turnStart.AddAbility(playRandomCard);
    }
}

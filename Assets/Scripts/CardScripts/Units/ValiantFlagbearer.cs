using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValiantFlagbearer : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        Revere revere = new Revere(unit);
        conditions.Add(revere);

        SequenceSystem.PlayRandomCardsFromDeck playCards =
            new SequenceSystem.PlayRandomCardsFromDeck(
                CardSelector.TypeFilter.Unit,
                costFilter: 4,
                costCompare: CardSelector.CostCompare.LessThanOrEqualTo);

        revere.AddAbility(playCards);
    }
}

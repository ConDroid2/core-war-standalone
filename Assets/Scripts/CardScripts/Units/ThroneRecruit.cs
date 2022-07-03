using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneRecruit : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        Revere revere = new Revere(unit);
        conditions.Add(revere);

        SequenceSystem.TargetAll targetInHand =
            new SequenceSystem.TargetAll(
                unit,
                CardSelector.HandFilter.MyHand,
                typeFilter: CardSelector.TypeFilter.Unit,
                subtypeFilter: "Warrior",
                originatingCard: CardSelector.OriginatingCard.Default);

        SequenceSystem.TargetAllInDeck targetInDeck =
            new SequenceSystem.TargetAllInDeck(
                typeFilter: CardSelector.TypeFilter.Unit,
                subtypeFilter: "Warrior");

        SequenceSystem.BuffInHand buffInHand = new SequenceSystem.BuffInHand(1, 1);
        SequenceSystem.BuffInDeck buffInDeck = new SequenceSystem.BuffInDeck(1, 1);

        targetInHand.AddAbility(buffInHand);
        targetInDeck.AddAbility(buffInDeck);

        revere.AddAbility(targetInHand);
        revere.AddAbility(targetInDeck);
    }
}

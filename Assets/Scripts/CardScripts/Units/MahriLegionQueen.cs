using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahriLegionQueen : CardScript
{
    public override void InPlaySetUp()
    {
        InPlayCardController card = GetComponent<InPlayCardController>();
        card.GetComponent<Legendary>().Initialize("Call to Arms");

        OnProphecyFulfilled onProphecy = new OnProphecyFulfilled(card);
        conditions.Add(onProphecy);

        SequenceSystem.TargetAll targetUnits = new SequenceSystem.TargetAll(card, zoneFilter: CardSelector.ZoneFilter.MyZones, subtypeFilter: "Warrior");
        SequenceSystem.TargetAll targetHand = new SequenceSystem.TargetAll(card, CardSelector.HandFilter.All, subtypeFilter: "Warrior");
        SequenceSystem.TargetAllInDeck targetDeck = new SequenceSystem.TargetAllInDeck(CardSelector.TypeFilter.Unit, subtypeFilter: "Warrior");

        SequenceSystem.BuffInPlay buffInPlay = new SequenceSystem.BuffInPlay(1, 1);
        SequenceSystem.BuffInHand buffInHand = new SequenceSystem.BuffInHand(1, 1);
        SequenceSystem.BuffInDeck buffInDeck = new SequenceSystem.BuffInDeck(1, 1);

        targetUnits.AddAbility(buffInPlay);
        targetHand.AddAbility(buffInHand);
        targetDeck.AddAbility(buffInDeck);

        onProphecy.AddAbility(targetUnits);
        onProphecy.AddAbility(targetHand);
        onProphecy.AddAbility(targetDeck);
    }
}

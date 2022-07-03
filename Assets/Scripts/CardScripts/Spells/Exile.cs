using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exile : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.Target target =
            new SequenceSystem.Target(
                card,
                zoneFilter: CardSelector.ZoneFilter.All,
                typeFilter: CardSelector.TypeFilter.Unit
            );
        target.optional = true;

        SequenceSystem.ReturnTargetToHand returnToHand = new SequenceSystem.ReturnTargetToHand();
        target.AddAbility(returnToHand);

        onPlay.AddAbility(target);
    }
}

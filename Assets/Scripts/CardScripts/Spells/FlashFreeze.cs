using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFreeze : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.Target target = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);

        SequenceSystem.AddStatus addStatus = new SequenceSystem.AddStatus("Rooted", 1);
        target.abilities.Add(addStatus);

        onPlay.AddAbility(target);
    }
}

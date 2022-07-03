using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthSpurt : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.Target targetAbility = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);

        SequenceSystem.BuffInPlay buff = new SequenceSystem.BuffInPlay(2, 2);

        targetAbility.abilities.Add(buff);

        onPlay.AddAbility(targetAbility);
    }
}

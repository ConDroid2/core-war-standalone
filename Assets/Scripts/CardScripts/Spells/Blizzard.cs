using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blizzard : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.TargetMultiple target = new SequenceSystem.TargetMultiple(card, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
        target.SetAmountOfTargets(3);
        target.SetTargetMode(SequenceSystem.TargetMultiple.TargetMode.UpTo);

        SequenceSystem.AddStatus addStatus = new SequenceSystem.AddStatus("Frozen", 1);
        target.abilities.Add(addStatus);

        onPlay.AddAbility(target);
    }
}

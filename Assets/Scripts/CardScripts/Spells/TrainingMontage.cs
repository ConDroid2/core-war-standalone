using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingMontage : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.BuffInPlay buff = new SequenceSystem.BuffInPlay(1, 1);

        SequenceSystem.Heal heal = new SequenceSystem.Heal();

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(card, 
            zoneFilter: CardSelector.ZoneFilter.MyZones, 
            typeFilter: CardSelector.TypeFilter.Unit);
        targetAll.abilities.Add(buff);
        targetAll.abilities.Add(heal);

        onPlay.AddAbility(targetAll);
    }
}

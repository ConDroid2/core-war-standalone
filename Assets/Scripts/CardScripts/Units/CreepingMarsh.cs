using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepingMarsh : CardScript
{
    public override void InPlaySetUp()
    {
        OnEnterPlay onPlay = new OnEnterPlay(GetComponent<InPlayCardController>());
        conditions.Add(onPlay);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(GetComponent<CardParent>(), zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);

        SequenceSystem.AddStatus addStatus = new SequenceSystem.AddStatus("Rooted", 1);
        targetAll.AddAbility(addStatus);
        onPlay.AddAbility(targetAll);
    }
}

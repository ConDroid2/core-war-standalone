using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningCold : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(card, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit, statusFilter: "Frozen");
        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("4", card));
        targetAll.AddAbility(damage);
        onPlay.AddAbility(targetAll);
    }
}

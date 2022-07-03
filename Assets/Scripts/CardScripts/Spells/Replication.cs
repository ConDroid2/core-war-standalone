using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replication : CardScript
{
    public override void InHandSetUp()
    {      
        CardController card = GetComponent<CardController>();
        Ignite ignite = new Ignite(card, 2);
        SequenceSystem.Replicate replicate = new SequenceSystem.Replicate(card);

        SequenceSystem.Target target = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit);
        target.abilities.Add(replicate);
        ignite.baseAbility.AddAbility(target);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(card, zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.All);
        targetAll.abilities.Add(replicate);
        ignite.ignitedAbility.AddAbility(targetAll);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmGlow : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();

        Ignite ignite = new Ignite(card, 1);
        SequenceSystem.RemoveStatuses removeStatuses = new SequenceSystem.RemoveStatuses();

        SequenceSystem.Target target = new SequenceSystem.Target(card,
            zoneFilter: CardSelector.ZoneFilter.MyZones,
            typeFilter: CardSelector.TypeFilter.Unit);
        target.abilities.Add(removeStatuses);
        ignite.baseAbility.AddAbility(target);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(card,
            zoneFilter: CardSelector.ZoneFilter.MyZones,
            typeFilter: CardSelector.TypeFilter.Unit);
        targetAll.abilities.Add(removeStatuses);
        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);
        ignite.ignitedAbility.AddAbility(draw);
        ignite.ignitedAbility.AddAbility(targetAll);
    }
}

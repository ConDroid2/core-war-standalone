using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raze : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();

        Ignite ignite = new Ignite(card, 2);

        SequenceSystem.Target targetOne = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Support);
        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(card, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Support);

        SequenceSystem.DestroyCard destroy = new SequenceSystem.DestroyCard();

        targetOne.AddAbility(destroy);
        targetAll.AddAbility(destroy);

        ignite.baseAbility.AddAbility(targetOne);
        ignite.ignitedAbility.AddAbility(targetAll);
    }
}

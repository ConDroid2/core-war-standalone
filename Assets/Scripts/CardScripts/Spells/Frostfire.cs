using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frostfire : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();

        Ignite ignite = new Ignite(card, 1);

        SequenceSystem.Target baseTarget = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);
        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("3", card));
        baseTarget.abilities.Add(damage);
        ignite.baseAbility.AddAbility(baseTarget);

        SequenceSystem.Target igniteTarget = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);
        SequenceSystem.AddStatus addStatus = new SequenceSystem.AddStatus("Rooted", 2);
        igniteTarget.abilities.Add(damage);
        igniteTarget.abilities.Add(addStatus);
        ignite.ignitedAbility.AddAbility(igniteTarget);
    }
}

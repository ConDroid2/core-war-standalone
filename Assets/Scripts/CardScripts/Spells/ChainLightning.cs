using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();

        OnPlay baseOnPlay = new OnPlay(card);
        conditions.Add(baseOnPlay);
        OnPlay igniteOnPlay = new OnPlay(card);
        conditions.Add(igniteOnPlay);

        SequenceSystem.Target target = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);
        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("3", card));
        target.abilities.Add(damage);
        baseOnPlay.AddAbility(target);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(card, zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit);       
        SequenceSystem.Damage igniteDamage = new SequenceSystem.Damage(new IntInput("3", card));
        targetAll.abilities.Add(igniteDamage);

        igniteOnPlay.AddAbility(targetAll);

        Ignite ignite = new Ignite(card, 2, baseOnPlay, igniteOnPlay);
    }
}

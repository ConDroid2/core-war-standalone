using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        Ignite ignite = new Ignite(card, 1);

        SequenceSystem.Target baseTarget = new SequenceSystem.Target(card, 
            zoneFilter: CardSelector.ZoneFilter.EnemyZones, 
            typeFilter: CardSelector.TypeFilter.Unit,
            originatingCard: CardSelector.OriginatingCard.TargetedAbility);
        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("1", card));
        baseTarget.abilities.Add(damage);
        ignite.baseAbility.AddAbility(baseTarget);

        SequenceSystem.Target igniteTarget = new SequenceSystem.Target(card, 
            zoneFilter: CardSelector.ZoneFilter.EnemyZones, 
            typeFilter: CardSelector.TypeFilter.Unit,
            originatingCard: CardSelector.OriginatingCard.TargetedAbility);
        SequenceSystem.Damage igniteDamage = new SequenceSystem.Damage(new IntInput("3", card));
        igniteTarget.abilities.Add(igniteDamage);
        ignite.ignitedAbility.AddAbility(igniteTarget);
    }
}

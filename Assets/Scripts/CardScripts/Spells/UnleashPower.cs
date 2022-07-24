using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnleashPower : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();

        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("IgniteCount", card));

        SequenceSystem.Target baseTarget = new SequenceSystem.Target(card, 
            zoneFilter: CardSelector.ZoneFilter.EnemyZones, 
            typeFilter: CardSelector.TypeFilter.Unit);
        baseTarget.abilities.Add(damage);

        SequenceSystem.TargetAll igniteTarget = new SequenceSystem.TargetAll(card, 
            zoneFilter: CardSelector.ZoneFilter.EnemyZones, 
            typeFilter: CardSelector.TypeFilter.Unit);
        igniteTarget.abilities.Add(damage);

        SequenceSystem.ChangeCost changeCost = new SequenceSystem.ChangeCost(setToAmount: 0);
        SequenceSystem.Aura costAura = new SequenceSystem.Aura(changeCost, null, card.isMine, CardSelector.HandFilter.MyHand);
        SequenceSystem.AuraOngoingEffect ongoingEffect = new SequenceSystem.AuraOngoingEffect(costAura);
        SequenceSystem.InitiateOngoingEffect initiateEffect = new SequenceSystem.InitiateOngoingEffect(ongoingEffect);


        Ignite ignite = new Ignite(card, 12);
        ignite.baseAbility.AddAbility(baseTarget);
        ignite.ignitedAbility.AddAbility(igniteTarget);
        ignite.ignitedAbility.AddAbility(initiateEffect);
    }
}

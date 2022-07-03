using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameLash : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();

        Ignite ignite = new Ignite(card, 2);

        //SequenceSystem.TargetMultiple baseTarget = new SequenceSystem.TargetMultiple(card, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
        //baseTarget.SetAmountOfTargets(2);
        //baseTarget.SetTargetMode(SequenceSystem.TargetMultiple.TargetMode.UpTo);
        //// SequenceSystem.Damage baseDamage = new SequenceSystem.Damage(2);
        //baseTarget.abilities.Add(baseDamage);
        //ignite.baseAbility.AddAbility(baseTarget);

        //SequenceSystem.TargetMultiple igniteTarget = new SequenceSystem.TargetMultiple(card, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
        //igniteTarget.SetAmountOfTargets(2);
        //igniteTarget.SetTargetMode(SequenceSystem.TargetMultiple.TargetMode.UpTo);
        //SequenceSystem.Damage igniteDamage = new SequenceSystem.Damage(4);
        //igniteTarget.abilities.Add(igniteDamage);
        //ignite.ignitedAbility.AddAbility(igniteTarget);
    }
}

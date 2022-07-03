using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrilTheMany : CardScript
{
    public override void InPlaySetUp()
    {
        GetComponent<Legendary>().Initialize("Omnipresence");

        InPlayCardController card = GetComponent<InPlayCardController>();
        OnIgnitedSpell onIgnited = new OnIgnitedSpell(card);
        conditions.Add(onIgnited);

        SequenceSystem.Summon summon = new SequenceSystem.Summon(1, "Tril, the Many", card.gameObject);
        onIgnited.AddAbility(summon);

        Ascend ascend = new Ascend(card);
        conditions.Add(ascend);

        SequenceSystem.Target target = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
        target.optional = true;

        SequenceSystem.TargetedTransformation transformation = new SequenceSystem.TargetedTransformation(card);
        target.abilities.Add(transformation);

        ascend.AddAbility(target);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empower : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        Ignite ignite = new Ignite(card, 2);

        SequenceSystem.Target baseTarget = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit);
        SequenceSystem.AddKeyword addKeyword = new SequenceSystem.AddKeyword("Swift", card);
        baseTarget.abilities.Add(addKeyword);

        SequenceSystem.Target igniteTarget = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit);
        SequenceSystem.RefreshActions refresh = new SequenceSystem.RefreshActions();
        igniteTarget.abilities.Add(refresh);

        ignite.baseAbility.AddAbility(baseTarget);
        ignite.ignitedAbility.AddAbility(igniteTarget);
    }
}

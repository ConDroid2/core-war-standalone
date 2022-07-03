using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManifestEnergy : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        Ignite ignite = new Ignite(card, 4);

        SequenceSystem.Summon baseSummon = new SequenceSystem.Summon(1, "Energy Golem", card.gameObject);
        ignite.baseAbility.AddAbility(baseSummon);

        SequenceSystem.Summon igniteSummon = new SequenceSystem.Summon(5, "Energy Golem", card.gameObject);
        ignite.ignitedAbility.AddAbility(igniteSummon);
    }
}

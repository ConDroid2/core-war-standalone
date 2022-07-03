using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningCantrip : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();

        SequenceSystem.Summon baseSummon = new SequenceSystem.Summon(1, "Toad", card.gameObject);

        SequenceSystem.Summon igniteSummon = new SequenceSystem.Summon(1, "Big Toad", card.gameObject);

        Ignite ignite = new Ignite(card, 1);
        ignite.baseAbility.AddAbility(baseSummon);
        ignite.ignitedAbility.AddAbility(igniteSummon);
    }
}

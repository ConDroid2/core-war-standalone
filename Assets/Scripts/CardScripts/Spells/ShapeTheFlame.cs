using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeTheFlame : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        Ignite ignite = new Ignite(card, 2);

        SequenceSystem.Summon baseSummon = new SequenceSystem.Summon(1, "Fire Sprite", card.gameObject);
        ignite.baseAbility.AddAbility(baseSummon);

        SequenceSystem.Summon igniteSummon = new SequenceSystem.Summon(1, "Blazing Elemental", card.gameObject);
        ignite.ignitedAbility.AddAbility(igniteSummon);
    }
}

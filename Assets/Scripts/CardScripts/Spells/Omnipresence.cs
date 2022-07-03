using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Omnipresence : CardScript
{
    public override void InHandSetUp()
    {
        Ignite ignite = new Ignite(GetComponent<CardController>(), 1);

        SequenceSystem.AddCardsToDeck addCards = new SequenceSystem.AddCardsToDeck(3, "Tril, the Many");

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);

        ignite.baseAbility.AddAbility(addCards);
        ignite.ignitedAbility.AddAbility(addCards);
        ignite.ignitedAbility.AddAbility(draw);
    }
}

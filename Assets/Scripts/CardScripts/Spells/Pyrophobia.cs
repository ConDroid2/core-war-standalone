using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyrophobia : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        Ignite ignite = new Ignite(card, 2);

        SequenceSystem.AddActionToOpponent baseAddAction = new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.AddCardsToDeck), new object[] {3, "Nightmare" });
        SequenceSystem.AddActionToOpponent igniteAddAction = new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.AddCardsToHand), new object[] { 3, "Nightmare" });

        ignite.baseAbility.AddAbility(baseAddAction);
        ignite.ignitedAbility.AddAbility(igniteAddAction);
    }
}

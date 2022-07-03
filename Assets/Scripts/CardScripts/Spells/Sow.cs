using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sow : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.AddActionToOpponent addNightmare = 
            new SequenceSystem.AddActionToOpponent(
                typeof(SequenceSystem.AddCardsToDeck),
                new object[] { 2, "Nightmare"});

        SequenceSystem.AddCardsToHand addReap = new SequenceSystem.AddCardsToHand(1, "Reap");

        onPlay.AddAbility(addNightmare);
        onPlay.AddAbility(addReap);
    }
}

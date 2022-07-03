using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoyodisGambit : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(3);
        onPlay.AddAbility(draw);

        SequenceSystem.IncreaseCurrentMagick increaseEnergy = new SequenceSystem.IncreaseCurrentMagick(3);
        onPlay.AddAbility(increaseEnergy);

        SequenceSystem.ChangeEndTurnButton changeButton = new SequenceSystem.ChangeEndTurnButton("Lose");
        onPlay.AddAbility(changeButton);   
    }
}

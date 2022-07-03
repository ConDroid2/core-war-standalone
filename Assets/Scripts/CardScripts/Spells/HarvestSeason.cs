using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestSeason : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(2);

        SequenceSystem.IncreaseLevel increaseEnergy = new SequenceSystem.IncreaseLevel(2);

        onPlay.AddAbility(draw);
        onPlay.AddAbility(increaseEnergy);
    }
}

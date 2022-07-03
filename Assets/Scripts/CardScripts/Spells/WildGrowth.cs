using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildGrowth : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        SequenceSystem.IncreaseLevel increase = new SequenceSystem.IncreaseLevel(3);

        onPlay.AddAbility(increase);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlossomingCharm : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        SequenceSystem.IncreaseLevel increase = new SequenceSystem.IncreaseLevel(1);

        onPlay.AddAbility(increase);
    }
}

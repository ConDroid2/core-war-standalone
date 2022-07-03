using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reshape : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        SequenceSystem.IncreaseCurrentMagick increase = new SequenceSystem.IncreaseCurrentMagick(amount: 3);

        onPlay.AddAbility(increase);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteAspiration : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(4);

        SequenceSystem.RefillMagick refill = new SequenceSystem.RefillMagick();

        onPlay.AddAbility(draw);
        onPlay.AddAbility(refill);
    }
}

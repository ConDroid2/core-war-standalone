using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyousVision : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(4);
        SequenceSystem.ChangeCost changeCost = new SequenceSystem.ChangeCost(setToAmount: 0);
        draw.AddAbility(changeCost);

        onPlay.AddAbility(draw);
    }
}

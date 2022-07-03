using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forecast : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        SequenceSystem.DrawRandomCards draw = new SequenceSystem.DrawRandomCards(amount: 2, keywordFilter: "Prophecy");

        onPlay.AddAbility(draw);
    }
}

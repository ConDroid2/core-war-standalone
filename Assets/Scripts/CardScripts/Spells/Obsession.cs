using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsession : CardScript
{
    public override void InHandSetUp()
    {
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(3);
        SequenceSystem.ChangeHandSize changeHandSize = new SequenceSystem.ChangeHandSize(-3);

        onPlay.AddAbility(draw);
        onPlay.AddAbility(changeHandSize);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepingSwarm : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.Summon summon = new SequenceSystem.Summon(3, "Worker Sylkan", card.gameObject);

        onPlay.AddAbility(summon);
    }
}

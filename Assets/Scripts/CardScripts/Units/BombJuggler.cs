using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombJuggler : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onPlay = new OnEnterPlay(unit);
        conditions.Add(onPlay);

        SequenceSystem.AddCardsToDeck addCards = new SequenceSystem.AddCardsToDeck(4, "Bomb");
        onPlay.AddAbility(addCards);
    }
}

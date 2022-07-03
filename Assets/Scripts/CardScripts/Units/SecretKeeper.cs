using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretKeeper : CardScript
{
    public override void InHandSetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onPlay = new OnEnterPlay(unit);
        conditions.Add(onPlay);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);
    }
}

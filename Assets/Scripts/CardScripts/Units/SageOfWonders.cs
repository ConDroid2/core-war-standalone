using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SageOfWonders : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();

        OnTurnEnd turnEnd = new OnTurnEnd(unit);
        conditions.Add(turnEnd);

        SequenceSystem.DrawToHandSize draw = new SequenceSystem.DrawToHandSize();
        turnEnd.AddAbility(draw);
    }
}

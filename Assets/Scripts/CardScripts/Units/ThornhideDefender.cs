using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornhideDefender : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        SequenceSystem.BuffInPlay buff = new SequenceSystem.BuffInPlay(2, 2, unit);

        SequenceSystem.BuffInPlay removeBuff = new SequenceSystem.BuffInPlay(-2, -2, unit);

        WhileCondition conditional = new WhileCondition(unit, "EnemyHasMoreUnits", buff, removeBuff);
        conditions.Add(conditional);
    }
}

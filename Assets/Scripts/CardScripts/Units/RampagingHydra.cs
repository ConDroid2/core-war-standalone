using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampagingHydra : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        SequenceSystem.DoubleStats doubleStats = new SequenceSystem.DoubleStats(unit);
        unit.AttackStack.Insert(0, doubleStats);
    }
}

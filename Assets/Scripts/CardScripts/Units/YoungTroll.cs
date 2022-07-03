using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoungTroll : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        // Create Condition
        Ascend ascend = new Ascend(GetComponent<InPlayCardController>());
        conditions.Add(ascend);

        // Create abilities
        SequenceSystem.Transformation transformation = new SequenceSystem.Transformation("Troll", unit);

        // Add ability to condition;
        ascend.AddAbility(transformation);
    }
}

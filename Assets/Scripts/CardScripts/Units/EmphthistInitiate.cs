using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmphthistInitiate : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnUnitStrengthIncreased onStrength = new OnUnitStrengthIncreased(unit);
        conditions.Add(onStrength);

        SequenceSystem.Transformation transformation = new SequenceSystem.Transformation("Learned Emphthist", unit);

        onStrength.AddAbility(transformation);
    }
}

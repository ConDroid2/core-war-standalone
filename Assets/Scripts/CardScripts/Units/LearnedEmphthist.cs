using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnedEmphthist : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnAnyUnitStrengthIncreased onStrength = new OnAnyUnitStrengthIncreased(unit);
        conditions.Add(onStrength);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);
        onStrength.AddAbility(draw);
    }
}

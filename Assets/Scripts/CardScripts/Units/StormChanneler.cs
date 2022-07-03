using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormChanneler : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnSpellCast onSpell = new OnSpellCast(unit);
        conditions.Add(onSpell);

        SequenceSystem.AddBuffToken addBuffToken = new SequenceSystem.AddBuffToken(unit, 1, 1, true);

        onSpell.AddAbility(addBuffToken);
    }
}

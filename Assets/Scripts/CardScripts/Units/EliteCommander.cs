using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteCommander : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        Revere revere = new Revere(unit);
        conditions.Add(revere);

        SequenceSystem.Summon summon = new SequenceSystem.Summon(1, "Throne Veteran", unit.gameObject);
        revere.AddAbility(summon);
    }
}

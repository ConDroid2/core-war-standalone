using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhalanxLegionnaire : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        Revere revere = new Revere(unit);
        conditions.Add(revere);

        SequenceSystem.BuffInPlay buff = new SequenceSystem.BuffInPlay(1, 1, unit);

        revere.AddAbility(buff);
    }
}

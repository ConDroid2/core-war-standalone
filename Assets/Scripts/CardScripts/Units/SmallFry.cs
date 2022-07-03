using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFry : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnDeath onDeath = new OnDeath(unit);
        conditions.Add(onDeath);

        SequenceSystem.Summon summon = new SequenceSystem.Summon(1, "Hungry Shark", unit.gameObject);
        onDeath.AddAbility(summon);
    }
}

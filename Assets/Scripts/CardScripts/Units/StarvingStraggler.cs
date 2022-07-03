using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarvingStraggler : CardScript
{
    public override void InPlaySetUp()
    {
        OnDeath onDeath = new OnDeath(GetComponent<UnitController>());
        conditions.Add(onDeath);

        SequenceSystem.Summon summon = new SequenceSystem.Summon(1, "Raven", gameObject);

        onDeath.AddAbility(summon);
    }
}

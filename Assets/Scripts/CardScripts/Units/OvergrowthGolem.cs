using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvergrowthGolem : CardScript
{
    public override void InPlaySetUp()
    {
        // Set up conditions
        Ascend ascend = new Ascend(GetComponent<InPlayCardController>());
        conditions.Add(ascend);

        // Set up abilities
        SequenceSystem.Summon ability = new SequenceSystem.Summon(1, "Replicating Sylkan", gameObject);

        // Add abilities to condition
        ascend.AddAbility(ability);
    }
}

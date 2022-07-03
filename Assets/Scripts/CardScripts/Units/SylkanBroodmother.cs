using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SylkanBroodmother : CardScript
{
    public override void InPlaySetUp()
    {
        OnDeath onDeath = new OnDeath(GetComponent<InPlayCardController>());
        conditions.Add(onDeath);

        SequenceSystem.Summon summon = new SequenceSystem.Summon(2, "Sylkan Larva", gameObject);

        onDeath.AddAbility(summon);
    }
}

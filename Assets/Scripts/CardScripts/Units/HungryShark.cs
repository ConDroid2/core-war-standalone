using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryShark : CardScript
{
    public override void InPlaySetUp()
    {
        OnDeath onDeath = new OnDeath(GetComponent<InPlayCardController>());
        conditions.Add(onDeath);

        SequenceSystem.Summon summon = new SequenceSystem.Summon(1, "River Sarassan", GetComponent<UnitController>().gameObject);
        onDeath.AddAbility(summon);
    }
}

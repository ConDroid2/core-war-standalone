using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LushGardens : CardScript
{
    public override void InPlaySetUp()
    {
        SupportController support = GetComponent<SupportController>();

        OnTurnEnd onTurnEnd = new OnTurnEnd(support);
        conditions.Add(onTurnEnd);

        SequenceSystem.IncreaseLevel increaseLevel = new SequenceSystem.IncreaseLevel(amount: 1);
        onTurnEnd.AddAbility(increaseLevel);
    }
}

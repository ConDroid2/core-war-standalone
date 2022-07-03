using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGrandLibrary : CardScript
{
    public override void InPlaySetUp()
    {
        SupportController support = GetComponent<SupportController>();

        OnTurnStart turnStart = new OnTurnStart(support);
        conditions.Add(turnStart);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);
        turnStart.AddAbility(draw);
    }
}

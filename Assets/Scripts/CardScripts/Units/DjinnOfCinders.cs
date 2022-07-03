using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DjinnOfCinders : CardScript
{
    public override void InPlaySetUp()
    {
        OnTurnStart turnStart = new OnTurnStart(GetComponent<InPlayCardController>());
        conditions.Add(turnStart);

        SequenceSystem.IncreaseIgniteCount increase = new SequenceSystem.IncreaseIgniteCount(1);
        turnStart.AddAbility(increase);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flustermage : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController controller = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(controller);
        conditions.Add(onEnterPlay);

        SequenceSystem.IncreaseIgniteCount increase = new SequenceSystem.IncreaseIgniteCount(1);
        onEnterPlay.AddAbility(increase);

        SequenceSystem.AddToAdvanceStack addToAdvance = new SequenceSystem.AddToAdvanceStack(controller, increase);
        onEnterPlay.AddAbility(addToAdvance);
    }
}

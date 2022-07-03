using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoldExplorer : CardScript
{
    public override void InPlaySetUp()
    {
        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);

        GetComponent<UnitController>().AdvanceStack.Add(draw);
    }
}

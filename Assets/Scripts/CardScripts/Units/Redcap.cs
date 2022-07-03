using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redcap : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        SequenceSystem.AddKeyword addKeyword = new SequenceSystem.AddKeyword("Immobile", unit);
        SequenceSystem.RemoveKeyword removeKeyword = new SequenceSystem.RemoveKeyword("Immobile", unit);

        WhileCondition whileCondition = new WhileCondition(
            GetComponent<InPlayCardController>(),
            "EnemyHasUnits",
            addKeyword,
            removeKeyword);

        conditions.Add(whileCondition);
            
    }
}

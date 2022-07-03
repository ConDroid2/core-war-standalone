using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhisperingSpectre : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnTurnStart turnStart = new OnTurnStart(unit);
        conditions.Add(turnStart);

        SequenceSystem.AddActionToOpponent addAction = new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.AddCardsToDeck), new object[] { 1, "Nightmare"});
        turnStart.AddAbility(addAction);
    }
}

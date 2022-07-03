using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MnemonicWraith : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);
        OnTurnStart turnStart = new OnTurnStart(unit);
        conditions.Add(turnStart);

        SequenceSystem.AddActionToOpponent addAction = new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.AddCardsToDeck), new object[] { 2, "Nightmare" });
        SequenceSystem.AddActionToOpponent enemyDraw = new SequenceSystem.AddActionToOpponent(typeof(SequenceSystem.DrawCards), new object[] { 1 });
        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);

        onEnterPlay.AddAbility(addAction);
        turnStart.AddAbility(draw);
        turnStart.AddAbility(enemyDraw);
    }
}

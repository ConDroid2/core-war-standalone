using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeonStrider : CardScript
{
    public override void InPlaySetUp()
    {
        OnEnterPlay onEnterPlay = new OnEnterPlay(GetComponent<InPlayCardController>());
        conditions.Add(onEnterPlay);

        SequenceSystem.ChangeEndTurnButton changeButton = new SequenceSystem.ChangeEndTurnButton("NextTurn");
        onEnterPlay.AddAbility(changeButton);
    }
}

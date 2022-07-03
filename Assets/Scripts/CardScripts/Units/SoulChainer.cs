using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulChainer : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);
        OnTurnStart onStart = new OnTurnStart(unit);
        conditions.Add(onStart);

        SequenceSystem.ExorciseSouls exorcise = new SequenceSystem.ExorciseSouls(exorciseMode: SequenceSystem.ExorciseSouls.Mode.Random, amountToExorcise: 3);
        onEnterPlay.AddAbility(exorcise);
        onStart.AddAbility(exorcise);
    }
}

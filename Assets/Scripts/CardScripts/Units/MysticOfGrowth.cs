using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticOfGrowth : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        int halfCreationLevel = (MagickManager.Instance.level["Green"] / 2);
        SequenceSystem.BuffInPlay buff = new SequenceSystem.BuffInPlay(halfCreationLevel, halfCreationLevel, unit);

        onEnterPlay.AddAbility(buff);
    }
}

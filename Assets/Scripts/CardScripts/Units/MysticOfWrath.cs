using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticOfWrath : CardScript
{
    public override void InPlaySetUp()
    {
        OnEnterPlay onEnterPlay = new OnEnterPlay(GetComponent<InPlayCardController>());
        conditions.Add(onEnterPlay);

        SequenceSystem.IncreaseIgniteCount increaseIgnite = new SequenceSystem.IncreaseIgniteCount(MagickManager.Instance.level["Red"] / 2);

        onEnterPlay.AddAbility(increaseIgnite);
    }
}

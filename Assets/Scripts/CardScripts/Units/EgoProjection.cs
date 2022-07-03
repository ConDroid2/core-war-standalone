using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoProjection : CardScript
{
    public override void InPlaySetUp()
    {
        OnEnterPlay onEnterPlay = new OnEnterPlay(GetComponent<InPlayCardController>());
        conditions.Add(onEnterPlay);

        SequenceSystem.DecreaseLevel decrease = new SequenceSystem.DecreaseLevel(1, "Red");

        onEnterPlay.AddAbility(decrease);
    }
}

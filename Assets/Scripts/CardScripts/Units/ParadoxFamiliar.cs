using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxFamiliar : CardScript
{
    public override void InPlaySetUp()
    {
        OnEnterPlay onEnterPlay = new OnEnterPlay(GetComponent<InPlayCardController>());
        conditions.Add(onEnterPlay);

        SequenceSystem.RefillMagick refill = new SequenceSystem.RefillMagick();

        onEnterPlay.AddAbility(refill);
    }
}

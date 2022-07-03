using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SequenceSystem;

public class BlossomDruid : CardScript
{
    public override void InPlaySetUp()
    {
        OnEnterPlay onEnterPlay = new OnEnterPlay(GetComponent<InPlayCardController>());
        conditions.Add(onEnterPlay);

        IncreaseLevel increase = new IncreaseLevel(1);
        
        onEnterPlay.AddAbility(increase);
    }
}

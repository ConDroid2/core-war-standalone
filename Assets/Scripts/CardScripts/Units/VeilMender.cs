using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeilMender : CardScript
{
    public override void InPlaySetUp()
    {
        OnEnterPlay onEnterPlay = new OnEnterPlay(GetComponent<InPlayCardController>());
        conditions.Add(onEnterPlay);

        SequenceSystem.ExorciseSouls exorcise = new SequenceSystem.ExorciseSouls();
        onEnterPlay.AddAbility(exorcise);
    }
}

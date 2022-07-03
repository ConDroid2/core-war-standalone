using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroveTender : CardScript
{
    public override void InPlaySetUp()
    {
        InPlayCardController card = GetComponent<InPlayCardController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(card);
        OnDeath onDeath = new OnDeath(card);
        conditions.Add(onEnterPlay);
        conditions.Add(onDeath);

        SequenceSystem.IncreaseLevel increase = new SequenceSystem.IncreaseLevel(1, "Green");
        SequenceSystem.DecreaseLevel decrease = new SequenceSystem.DecreaseLevel(1, "Green");



        onEnterPlay.AddAbility(increase);
        onDeath.AddAbility(decrease);
    }
}

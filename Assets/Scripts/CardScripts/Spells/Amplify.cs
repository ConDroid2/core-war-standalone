using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amplify : CardScript
{
    Ignite ignite;
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();

        OnPlay baseOnPlay = new OnPlay(card);
        conditions.Add(baseOnPlay);
        SequenceSystem.IncreaseCurrentMagick baseIncrease = new SequenceSystem.IncreaseCurrentMagick(2, "Red");
        baseOnPlay.AddAbility(baseIncrease);

        OnPlay igniteOnPlay = new OnPlay(card);
        conditions.Add(igniteOnPlay);
        SequenceSystem.IncreaseCurrentMagick igniteIncrease = new SequenceSystem.IncreaseCurrentMagick(4, "Red");
        igniteOnPlay.AddAbility(igniteIncrease);

        ignite = new Ignite(card, 3, baseOnPlay, igniteOnPlay);
    }
}

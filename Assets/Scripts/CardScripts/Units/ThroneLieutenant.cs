using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneLieutenant : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onPlay = new OnEnterPlay(unit);
        conditions.Add(onPlay);

        SequenceSystem.DrawRandomCards draw = new SequenceSystem.DrawRandomCards(1, CardSelector.TypeFilter.Unit, subtypeFilter: new StringInput("Warrior"));
        draw.AddAbility(new SequenceSystem.BuffInHand(1, 1));

        onPlay.AddAbility(draw);     
    }
}

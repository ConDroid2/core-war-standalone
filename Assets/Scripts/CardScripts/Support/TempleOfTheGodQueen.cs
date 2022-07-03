using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleOfTheGodQueen : CardScript
{
    public override void InPlaySetUp()
    {
        SupportController support = GetComponent<SupportController>();

        OnEnterPlay onEnterPlay = new OnEnterPlay(support);
        conditions.Add(onEnterPlay);

        Revere revere = new Revere(support);
        conditions.Add(revere);

        SequenceSystem.DrawRandomCards draw = new SequenceSystem.DrawRandomCards(costFilter: 5, costCompare: CardSelector.CostCompare.GreaterThanOrEqualTo);
        onEnterPlay.AddAbility(draw);
        revere.AddAbility(draw);
    }
}

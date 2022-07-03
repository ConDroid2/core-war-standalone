using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandsOfTime : CardScript
{
    public override void InPlaySetUp()
    {
        SupportController support = GetComponent<SupportController>();

        OnSpellProphesied onProphesy = new OnSpellProphesied(support);
        conditions.Add(onProphesy);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);
        onProphesy.AddAbility(draw);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaerieHearth : CardScript
{
    public override void InPlaySetUp()
    {
        SupportController support = GetComponent<SupportController>();

        OnSpellCast onSpell = new OnSpellCast(support);
        conditions.Add(onSpell);

        SequenceSystem.Summon summon = new SequenceSystem.Summon(1, "Faerie", gameObject);
        onSpell.AddAbility(summon);
    }
}

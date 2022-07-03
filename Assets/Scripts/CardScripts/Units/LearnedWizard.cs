using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnedWizard : CardScript
{
    public override void InPlaySetUp()
    {
        SequenceSystem.DrawRandomCards draw = new SequenceSystem.DrawRandomCards(amount: 1, typeFilter: CardSelector.TypeFilter.Spell);

        InPlayCardController card = GetComponent<InPlayCardController>();
        OnSpellCast onCast = new OnSpellCast(card);
        conditions.Add(onCast);
        OnSpellProphesied onProphesied = new OnSpellProphesied(card);
        conditions.Add(onProphesied);

        onCast.AddAbility(draw);
        onProphesied.AddAbility(draw);
    }
}

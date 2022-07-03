using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experimentation : CardScript
{
    public override void InHandSetUp()
    {
        Ignite ignite = new Ignite(GetComponent<CardController>(), 6);

        SequenceSystem.DrawRandomCards baseDraw = new SequenceSystem.DrawRandomCards(amount: 1, typeFilter: CardSelector.TypeFilter.Spell, costFilter: 1, costCompare: CardSelector.CostCompare.EqualTo);
        ignite.baseAbility.AddAbility(baseDraw);

        SequenceSystem.DrawRandomCards igniteDraw = new SequenceSystem.DrawRandomCards(amount: 4, typeFilter: CardSelector.TypeFilter.Spell);
        ignite.ignitedAbility.AddAbility(igniteDraw);
    }
}

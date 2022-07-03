using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagerApprentice : CardScript
{
    public override void InPlaySetUp()
    {
        Ascend ascend = new Ascend(GetComponent<InPlayCardController>());
        conditions.Add(ascend);

        SequenceSystem.DrawRandomCards draw = new SequenceSystem.DrawRandomCards(amount: 1, typeFilter: CardSelector.TypeFilter.Spell);
        ascend.AddAbility(draw);

        SequenceSystem.Transformation transformation = new SequenceSystem.Transformation("Learned Wizard", GetComponent<UnitController>());
        ascend.AddAbility(transformation);
    }
}

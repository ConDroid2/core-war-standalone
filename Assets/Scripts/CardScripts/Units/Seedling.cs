using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seedling : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();

        OnTurnStart turnStart = new OnTurnStart(GetComponent<InPlayCardController>());
        conditions.Add(turnStart);

        SequenceSystem.ConditionalWrapper conditional = new SequenceSystem.ConditionalWrapper(new IntInput("MyResilience", unit), ">", new IntInput("4", unit));

        SequenceSystem.BuffInPlay buff = new SequenceSystem.BuffInPlay(0, 1, unit);
        SequenceSystem.Transformation transformation = new SequenceSystem.Transformation("Treant Defender", unit);
        conditional.AddAbility(transformation);

        turnStart.AddAbility(buff);
        turnStart.AddAbility(conditional);
    }
}

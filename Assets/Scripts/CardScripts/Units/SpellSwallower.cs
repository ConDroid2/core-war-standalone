using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSwallower : CardScript
{

    public override void InPlaySetUp()
    {
        //UnitController unit = GetComponent<UnitController>();
        //OnAttemptedSpellCast onSpell = new OnAttemptedSpellCast(unit);
        //conditions.Add(onSpell);

        //SequenceSystem.CounterSpell counter = new SequenceSystem.CounterSpell();
        //SequenceSystem.BuffInPlay buff = new SequenceSystem.BuffInPlay(1, 1, unit);
        //SequenceSystem.ConditionalWrapper conditionalWrapper = new SequenceSystem.ConditionalWrapper(() => { return unit.cardData.currentStrength < 3; });
        //conditionalWrapper.AddAbility(counter);
        //conditionalWrapper.AddAbility(buff);

        //onSpell.AddAbility(conditionalWrapper);
    }
}

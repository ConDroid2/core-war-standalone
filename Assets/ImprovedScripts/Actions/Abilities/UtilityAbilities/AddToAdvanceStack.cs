using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class AddToAdvanceStack : GameAction
    {
        UnitController unit;
        Ability abilityToAdd;
        public AddToAdvanceStack(UnitController unit, Ability abilityToAdd)
        {
            this.unit = unit;
            this.abilityToAdd = abilityToAdd;
        }

        public AddToAdvanceStack(AddToAdvanceStack template)
        {
            unit = template.unit;
            abilityToAdd = template.abilityToAdd;
        }

        public override void PerformGameAction()
        {
            unit.AdvanceStack.Add(abilityToAdd);
            OnEnd();
        }
    }
}

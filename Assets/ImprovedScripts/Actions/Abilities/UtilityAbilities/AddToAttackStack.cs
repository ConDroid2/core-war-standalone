using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class AddToAttackStack : Ability
    {
        UnitController unit;
        Ability abilityToAdd;
        public AddToAttackStack(UnitController unit, Ability abilityToAdd)
        {
            this.unit = unit;
            this.abilityToAdd = abilityToAdd;
        }

        public AddToAttackStack(AddToAttackStack template)
        {
            unit = template.unit;
            abilityToAdd = template.abilityToAdd;
        }

        public override void PerformGameAction()
        {
            unit.AttackStack.Add(abilityToAdd);
            OnEnd();
        }
    }
}

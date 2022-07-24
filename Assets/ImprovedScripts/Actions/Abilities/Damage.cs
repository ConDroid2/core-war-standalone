using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class Damage : TargetedAbility
    {
        int amount = 0;

        private IntInput intInput;
        public Damage(IntInput intInput)
        {
            this.intInput = intInput;
        }

        public Damage(Damage template)
        {
            intInput = template.intInput;
            target = template.target;
        }

        public int DefaultGetAmount()
        {
            return amount;
        }

        public override void PerformGameAction()
        {
            target.GetComponent<UnitController>().takeDamage(intInput.Value);
            OnEnd();
        }
    }
}

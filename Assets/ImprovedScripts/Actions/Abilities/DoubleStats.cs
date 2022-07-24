using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class DoubleStats : TargetedAbility
    {
        private UnitController unit;
        public DoubleStats(UnitController unit)
        {
            this.unit = unit;
        }

        public DoubleStats(DoubleStats template)
        {
            unit = template.unit;
        }

        public override void PerformGameAction()
        {
            int maxResilienceIncrease = Mathf.Abs((unit.cardData.currentResilience * 2) - unit.cardData.currentResilience);
            int newDamage = unit.cardData.currentStrength * 2;

            unit.IncreaseMaxResilience(maxResilienceIncrease);
            unit.ChangeDamage(newDamage);

            OnEnd();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class Heal : TargetedAbility
    {
        public Heal()
        {
            
        }

        public Heal(Heal template)
        {
            target = template.target;
        }

        public override void PerformGameAction()
        {
            UnitController card = target.GetComponent<UnitController>();
            card.ChangeResilience(card.cardData.maxResilience);

            OnEnd();
        }
    }
}

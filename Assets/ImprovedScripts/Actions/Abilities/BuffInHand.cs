using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class BuffInHand : TargetedAbility
    {
        int damageIncrease;
        int resilienceIncrease;
        public BuffInHand(int damageIncrease, int resilienceIncrease)
        {
            this.damageIncrease = damageIncrease;
            this.resilienceIncrease = resilienceIncrease;
        }

        public BuffInHand(BuffInHand template)
        {
            damageIncrease = template.damageIncrease;
            resilienceIncrease = template.resilienceIncrease;
            target = template.target;
        }

        public override void PerformGameAction()
        {
            CardController card = target.GetComponent<CardController>();

            card.ChangeResilience(resilienceIncrease);
            card.ChangeStrength(damageIncrease);

            OnEnd();
        }
    }
}

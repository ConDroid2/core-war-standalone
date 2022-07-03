using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class BuffInDeck : InDeckTargetedAbility
    {
        int strengthIncrease;
        int resilienceIncrease;
        public BuffInDeck(int strengthIncrease, int resilienceIncrease)
        {
            this.strengthIncrease = strengthIncrease;
            this.resilienceIncrease = resilienceIncrease;
        }

        public BuffInDeck(BuffInDeck template)
        {
            strengthIncrease = template.strengthIncrease;
            resilienceIncrease = template.resilienceIncrease;
            target = template.target;
        }

        public override void PerformGameAction()
        {
            target.currentResilience += resilienceIncrease;
            target.maxResilience += resilienceIncrease;
            target.currentStrength += strengthIncrease;

            OnEnd();
        }
    }
}

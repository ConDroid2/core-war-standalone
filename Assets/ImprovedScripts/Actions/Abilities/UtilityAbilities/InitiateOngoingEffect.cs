using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SequenceSystem 
{
    public class InitiateOngoingEffect : Ability
    {
        OngoingEffectWrapper ongoingEffect;
        public InitiateOngoingEffect(OngoingEffectWrapper ongoingEffect)
        {
            this.ongoingEffect = ongoingEffect; 
        }

        public InitiateOngoingEffect(InitiateOngoingEffect template)
        {
            ongoingEffect = template.ongoingEffect;
        }

        public override void PerformGameAction()
        {
            OngoingEffectManager.Instance.AddOngoingEffect(ongoingEffect);

            OnEnd();
        }
    }
}

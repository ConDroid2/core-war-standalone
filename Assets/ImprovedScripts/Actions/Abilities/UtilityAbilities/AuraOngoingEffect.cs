using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem
{
    public class AuraOngoingEffect : OngoingEffectWrapper
    {
        Aura aura;

        public AuraOngoingEffect(Aura aura, WhenToEnd whenToEnd = WhenToEnd.Never)
        {
            this.aura = aura;
            this.whenToEnd = whenToEnd;
        }

        public override void SetUp()
        {
            MainSequenceManager.Instance.Add(aura);
        }

        public override void EndEffect()
        {
            aura.RemoveAura();
        }
    }
}

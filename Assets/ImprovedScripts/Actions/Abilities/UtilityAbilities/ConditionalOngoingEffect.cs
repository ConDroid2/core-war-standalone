using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem
{
    public class ConditionalOngoingEffect : OngoingEffectWrapper
    {
        private List<AbilityCondition> conditions = new List<AbilityCondition>();

        public ConditionalOngoingEffect(WhenToEnd whenToEnd = WhenToEnd.Never)
        {
            this.whenToEnd = whenToEnd;
        }

        public void AddCondition(AbilityCondition condition)
        {
            conditions.Add(condition);
            condition.Delete();
        }

        public override void EndEffect()
        {
            foreach(AbilityCondition condition in conditions)
            {
                condition.Delete();
            }
        }
    }
}

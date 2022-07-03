using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SequenceSystem 
{
    public class OngoingEffectWrapper
    {
        public enum WhenToEnd { TurnStart, TurnEnd, Never }
        public WhenToEnd whenToEnd = WhenToEnd.Never;

        public Action OnEffectEnded;

        public OngoingEffectWrapper()
        {
        }

        public virtual void SetUp()
        {
        }

        public virtual void EndEffect()
        {
        }
    }
}

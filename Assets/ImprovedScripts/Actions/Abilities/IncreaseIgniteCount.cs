using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class IncreaseIgniteCount : Ability
    {
        int amount;
        public IncreaseIgniteCount(int amount)
        {
            this.amount = amount;
        }

        public IncreaseIgniteCount(IncreaseIgniteCount template)
        {
            amount = template.amount;
        }

        public override void PerformGameAction()
        {
            IgniteManager.Instance.IncreaseIgniteCount(amount);

            OnEnd();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ReturnTargetToHand : TargetedAbility
    {
        public ReturnTargetToHand()
        {
            
        }

        public ReturnTargetToHand(ReturnTargetToHand template) : base(template)
        {
            
        }

        public override void PerformGameAction()
        {
            MainSequenceManager.Instance.Add(target.GetComponent<InPlayCardController>().returnToHand);
            OnEnd();
        }
    }
}

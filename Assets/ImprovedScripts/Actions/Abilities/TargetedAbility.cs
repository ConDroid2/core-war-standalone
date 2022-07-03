using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class TargetedAbility : Ability
    {
        protected GameObject target;
        public TargetedAbility()
        {
            
        }

        public TargetedAbility(TargetedAbility template)
        {
            target = template.target;
        }

        public override void PerformGameAction()
        {
            
        }

        public virtual void SetTarget(GameObject target)
        {
            this.target = target;
        }
    }
}

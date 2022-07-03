using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class InDeckTargetedAbility : Ability
    {
        protected Card target;
        public InDeckTargetedAbility()
        {
            
        }

        public InDeckTargetedAbility(InDeckTargetedAbility template)
        {
            target = template.target;
        }

        public override void PerformGameAction()
        {
            
        }

        public void SetTarget(Card target)
        {
            this.target = target;
        }
    }
}

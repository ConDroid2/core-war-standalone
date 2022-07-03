using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class DestroyCard : TargetedAbility
    {
        public DestroyCard()
        {
            
        }

        public DestroyCard(DestroyCard template)
        {
            target = template.target;
        }

        public override void PerformGameAction()
        {
            InPlayCardController controller = target.GetComponent<InPlayCardController>();
            MainSequenceManager.Instance.AddNext(controller.destroy);

            OnEnd();
        }
    }
}

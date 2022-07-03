using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class DiscardCard : TargetedAbility
    {
        public DiscardCard()
        {
            
        }

        public DiscardCard(DiscardCard template)
        {
            target = template.target;
        }

        public override void PerformGameAction()
        {
            CardController cardController = target.GetComponent<CardController>();
            MainSequenceManager.Instance.AddNext(cardController.discard);

            OnEnd();
        }
    }
}

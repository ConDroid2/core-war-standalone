using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class PlaySelectedCard : TargetedAbility
    {
        public PlaySelectedCard()
        {
            
        }

        public PlaySelectedCard(PlaySelectedCard template) : base(template)
        {
            
        }

        public override void PerformGameAction()
        {
            CardController card = target.GetComponent<CardController>();
            card.Play();
            OnEnd();
        }
    }
}

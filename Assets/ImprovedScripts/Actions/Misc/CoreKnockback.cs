using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class CoreKnockback : GameAction
    {
        public CoreKnockback()
        {
            
        }

        public CoreKnockback(CoreKnockback template)
        {
            
        }

        public override void PerformGameAction()
        {
            foreach(UnitController inPlayCard in Player.Instance.zones[1].cards.cards)
            {
                MainSequenceManager.Instance.AddNext(inPlayCard.unitMoveBack);
            }

            OnEnd();
        }
    }
}

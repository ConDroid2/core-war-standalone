using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class RefillMagick : Ability
    {
        public RefillMagick()
        {
            
        }

        public RefillMagick(RefillMagick template)
        {
            
        }

        public override void PerformGameAction()
        {
            MainSequenceManager.Instance.AddNext(Player.Instance.resetMagick);
            OnEnd();
        }
    }
}

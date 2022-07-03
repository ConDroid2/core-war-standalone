using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class UnitPlay : GameAction
    {
        public UnitPlay()
        {
            
        }

        public UnitPlay(UnitPlay template) : base(template)
        {
        }

        public override void PerformGameAction()
        {
            OnEnd();
        }
    }
}

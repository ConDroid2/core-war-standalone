using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ChangeHandSize : Ability
    {
        int changeAmount;
        public ChangeHandSize(int changeAmount)
        {
            this.changeAmount = changeAmount;
        }

        public ChangeHandSize(ChangeHandSize template)
        {
            changeAmount = template.changeAmount;
        }

        public override void PerformGameAction()
        {
            Player.Instance.handSize += changeAmount;
            OnEnd();
        }
    }
}

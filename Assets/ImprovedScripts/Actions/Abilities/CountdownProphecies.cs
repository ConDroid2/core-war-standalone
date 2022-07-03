using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class CountdownProphecies : GameAction
    {
        int amount;
        public CountdownProphecies(int amount)
        {
            this.amount = amount;
        }

        public CountdownProphecies(CountdownProphecies template)
        {
            amount = template.amount;
        }

        public override void PerformGameAction()
        {
            ProphecyManager.Instance.CountdownAllPropheciesOnce();
            OnEnd();
        }
    }
}

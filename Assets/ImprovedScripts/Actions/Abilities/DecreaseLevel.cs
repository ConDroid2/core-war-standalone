using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class DecreaseLevel : Ability
    {
        int amount = 0;
        string color = "";

        public DecreaseLevel(int amount, string color)
        {
            this.amount = amount;
            this.color = color;
        }

        public DecreaseLevel(DecreaseLevel template)
        {
            amount = template.amount;
            color = template.color;
        }

        public override void PerformGameAction()
        {
            MagickManager.Instance.ChangeLevel(color, amount * -1);

            OnEnd();
        }
    }
}

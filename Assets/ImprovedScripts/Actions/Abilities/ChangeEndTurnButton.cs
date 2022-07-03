using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ChangeEndTurnButton : Ability
    {
        string changeTo;
        public ChangeEndTurnButton(string changeTo)
        {
            this.changeTo = changeTo;
        }

        public ChangeEndTurnButton(ChangeEndTurnButton template)
        {
            changeTo = template.changeTo;
        }

        public override void PerformGameAction()
        {
            if(changeTo == "Lose")
            {
                MultiUseButton.Instance.ChangeToLose();
            }
            else if(changeTo == "NextTurn")
            {
                MultiUseButton.Instance.ChangeToNextTurn();
            }

            OnEnd();
        }
    }
}

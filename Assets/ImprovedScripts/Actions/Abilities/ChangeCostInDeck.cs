using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{    
    public class ChangeCostInDeck : InDeckTargetedAbility
    {
        private string school;
        private int changeBy;
        private int setToAmount;
        public ChangeCostInDeck(string school = "", int changeBy = 0, int setToAmount = -1)
        {
            this.school = school;
            this.changeBy = changeBy;
            this.setToAmount = setToAmount;
        }

        public ChangeCostInDeck(ChangeCostInDeck template)
        {
            school = template.school;
            changeBy = template.changeBy;
            setToAmount = template.setToAmount;
        }

        public override void PerformGameAction()
        {
            if (school != "")
            {
                if (changeBy != 0)
                {
                    target.cost[school] += changeBy;
                }
                else if (setToAmount != 0)
                {
                    target.cost[school] = setToAmount;
                }
            }
            else
            {
                foreach (string color in MagickManager.Instance.costColors)
                {
                    if (changeBy != 0)
                    {
                        target.cost[color] += changeBy;
                    }
                    else if (setToAmount != -1)
                    {
                        target.cost[color] = setToAmount;
                    }
                }
            }

            OnEnd();
        }
    }
}

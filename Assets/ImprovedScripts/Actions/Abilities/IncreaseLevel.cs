using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class IncreaseLevel : Ability
    {
        int amount;
        string school;
        public IncreaseLevel(int amount, string school = "")
        {
            this.amount = amount;
            this.school = school;
        }

        public IncreaseLevel(IncreaseLevel template)
        {
            amount = template.amount;
            school = template.school;
        }

        public override void PerformGameAction()
        {
            if (school == "")
            {
                for (int i = 0; i < amount; i++)
                {
                    MainSequenceManager.Instance.AddNext(Player.Instance.gainLevel);
                }
            }
            else
            {
                MagickManager.Instance.ChangeLevel(school, amount);
            }

            OnEnd();
        }
    }
}

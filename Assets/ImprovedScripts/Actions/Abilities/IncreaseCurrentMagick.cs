using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class IncreaseCurrentMagick : Ability
    {
        int amount;
        string school;

        public delegate int GetAmount();
        public GetAmount getAmount;
        public IncreaseCurrentMagick(int amount, string school = "")
        {
            this.amount = amount;
            this.school = school;
            getAmount = DefaultGetAmount;
        }

        public IncreaseCurrentMagick(GetAmount getAmount, string school)
        {
            this.getAmount = getAmount;
            this.school = school;
        }

        public IncreaseCurrentMagick(IncreaseCurrentMagick template)
        {
            amount = template.amount;
            school = template.school;
            getAmount = template.getAmount;
        }

        public override void PerformGameAction()
        {
            if (school == "")
            {
                MagickManager.Instance.OnCurrentChange += HandleCurrentChange;
                MagickManager.Instance.ChangeMode(MagickManager.Mode.IncreaseCurrent);
            }
            else
            {
                MagickManager.Instance.ChangeCurrent(school, getAmount());
                OnEnd();
            }    
        }

        public void HandleCurrentChange(string color, int amount)
        {
            this.amount--;
            Debug.Log("Increasing current magick");

            if(this.amount == 0)
            {
                MagickManager.Instance.OnCurrentChange -= HandleCurrentChange;
                MagickManager.Instance.ChangeMode(MagickManager.Mode.Default);

                OnEnd();
            }
        }

        public int DefaultGetAmount()
        {
            return amount;
        }
    }
}

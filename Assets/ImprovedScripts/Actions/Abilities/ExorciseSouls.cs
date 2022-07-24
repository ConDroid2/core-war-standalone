using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ExorciseSouls : Ability
    {
        public enum Mode { All, Random }
        private Mode exorciseMode;
        private int amountToExorcise;
        public ExorciseSouls(Mode exorciseMode = Mode.All, int amountToExorcise = 0)
        {
            this.exorciseMode = exorciseMode;
            this.amountToExorcise = amountToExorcise;
        }

        public ExorciseSouls(ExorciseSouls template)
        {
            exorciseMode = template.exorciseMode;
            amountToExorcise = template.amountToExorcise;
        }

        public override void PerformGameAction()
        {
            List<InPlayCardController> souls = new List<InPlayCardController>(UnderworldManager.Instance.souls);

            if (exorciseMode == Mode.All)
            {
                foreach (InPlayCardController soul in souls)
                {
                    UnderworldManager.Instance.ExorciseFromUnderworld(soul);
                }
            }
            else if(exorciseMode == Mode.Random)
            {
                for (int i = 0; i < amountToExorcise; i++)
                {
                    if (souls.Count > 0)
                    {
                        int randomIndex = Random.Range(0, souls.Count);
                        UnderworldManager.Instance.ExorciseFromUnderworld(souls[randomIndex]);
                        souls.RemoveAt(randomIndex);
                    }
                }
            }

            OnEnd();
        }
    }
}

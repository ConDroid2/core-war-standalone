using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ResurrectMe : Ability
    {
        UnitController unit;
        public ResurrectMe(UnitController unit)
        {
            this.unit = unit;
        }

        public ResurrectMe(ResurrectMe template)
        {
            unit = template.unit;
        }

        public override void PerformGameAction()
        {
            if (UnderworldManager.Instance.souls.Contains(unit))
            {
                UnitController inPlayCard = InPlayCardPool.Instance.Get();
                inPlayCard.turnPlayed = MatchManager.Instance.currentTurn;
                inPlayCard.SetUpCardFromName(unit.cardData.name);
                inPlayCard.gameObject.SetActive(true);

                UnderworldManager.Instance.ExorciseFromUnderworld(unit);

                MainSequenceManager.Instance.Add(inPlayCard.unitAdvance);
                MainSequenceManager.Instance.Add(inPlayCard.unitPlay);
            }

            OnEnd();
        }
    }
}

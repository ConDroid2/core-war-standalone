using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ResurrectRandomSoul : Ability
    {
        public ResurrectRandomSoul()
        {
            
        }

        public ResurrectRandomSoul(ResurrectRandomSoul template)
        {
            
        }

        public override void PerformGameAction()
        {
            List<InPlayCardController> souls = new List<InPlayCardController>(UnderworldManager.Instance.souls);
            souls.RemoveAll((card) => { return !card.isMine; });

            if (souls.Count > 0)
            {
                int randomIndex = Random.Range(0, souls.Count);
                InPlayCardController soulToResurrect = souls[randomIndex];

                UnitController inPlayCard = InPlayCardPool.Instance.Get();
                inPlayCard.turnPlayed = MatchManager.Instance.currentTurn;
                inPlayCard.SetUpCardFromName(soulToResurrect.cardData.name);
                inPlayCard.gameObject.SetActive(true);

                UnderworldManager.Instance.ExorciseFromUnderworld(soulToResurrect);

                MainSequenceManager.Instance.Add(inPlayCard.unitAdvance);
                MainSequenceManager.Instance.Add(inPlayCard.unitPlay);
            }

            OnEnd();
        }
    }
}

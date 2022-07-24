using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem
{
    public class InHandUnitPlay : GameAction
    {
        CardController cardController;

        public InHandUnitPlay(CardController card)
        {
            cardController = card;
        }

        public InHandUnitPlay(InHandUnitPlay template)
        {
            cardController = template.cardController;
        }

        public override void PerformGameAction()
        {
            

            UnitController inPlayCard = InPlayCardPool.Instance.Get(cardController.transform.position);
            inPlayCard.turnPlayed = MatchManager.Instance.currentTurn;
            inPlayCard.SetUpCardFromJson(cardController.cardData.GetJson());
            inPlayCard.gameObject.SetActive(cardController);

            cardController.Remove();
            MainSequenceManager.Instance.Add(inPlayCard.unitAdvance);
            MainSequenceManager.Instance.Add(inPlayCard.unitPlay);

            TriggerManager.Instance.InvokeOnCardPlayed(inPlayCard.cardData);

            OnEnd();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class InHandSupportPlay : GameAction
    {
        CardController cardController;
        int zoneToPlayTo = 0;
        public InHandSupportPlay(CardController cardController)
        {
            this.cardController = cardController;
        }

        public InHandSupportPlay(InHandSupportPlay template)
        {
            cardController = template.cardController;
            zoneToPlayTo = template.zoneToPlayTo;
        }

        public override void PerformGameAction()
        {
            SupportController newCard = Photon.Pun.PhotonNetwork.Instantiate("Prefabs/Support", Vector3.zero, Quaternion.identity).GetComponent<SupportController>();
            newCard.transform.position = cardController.transform.position;
            newCard.SetUpCardFromJson(cardController.cardData.GetJson());
            newCard.gameObject.SetActive(cardController);
            newCard.SetCurrentZoneNum(zoneToPlayTo);

            cardController.Remove();
            MainSequenceManager.Instance.Add(newCard.addToCurrentZone);
            TriggerManager.Instance.InvokeOnCardPlayed(newCard.cardData);
            
            OnEnd();
        }

        public void SetZoneToPlayTo(int newZone)
        {
            zoneToPlayTo = newZone;
        }
    }
}

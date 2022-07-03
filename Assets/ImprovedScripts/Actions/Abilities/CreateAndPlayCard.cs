using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem
{
    public class CreateAndPlayCard : Ability
    {
        private string cardToPlay;
        public delegate string GetCardName();
        private GetCardName getCardName;
        public CreateAndPlayCard(string cardToPlay, GameObject gameObject)
        {
            this.cardToPlay = cardToPlay;
            getCardName = DefaultGetCardName;
            this.gameObject = gameObject;
        }

        public CreateAndPlayCard(GetCardName getCardName, GameObject gameObject)
        {
            this.getCardName = getCardName;
            this.gameObject = gameObject;
        }

        public CreateAndPlayCard(CreateAndPlayCard template)
        {
            cardToPlay = template.cardToPlay;
            getCardName = template.getCardName;
            gameObject = template.gameObject;
        }

        public string DefaultGetCardName()
        {
            return cardToPlay;
        }

        public override void PerformGameAction()
        {
            CardController card = CardPool.Instance.Get();
            string cardName = getCardName();

            card.gameObject.SetActive(true);
            card.cardData = new Card(CardLibrary.Instance.library[cardName]);
            card.transform.position = gameObject.transform.position;
            card.initialPos = gameObject.transform.position;

            object[] rpcData = { cardName, false };
            card.photonView.RPC("SetUpCardFromName", Photon.Pun.RpcTarget.All, rpcData);

            card.Play();
            OnEnd();
        }
    }
}

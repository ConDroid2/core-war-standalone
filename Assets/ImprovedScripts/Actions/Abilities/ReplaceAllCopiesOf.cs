using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ReplaceAllCopiesOf : GameAction
    {
        string cardToReplace;
        string cardToReplaceWith;
        public ReplaceAllCopiesOf(string cardToReplace, string cardToReplaceWith)
        {
            this.cardToReplace = cardToReplace;
            this.cardToReplaceWith = cardToReplaceWith;
        }

        public ReplaceAllCopiesOf(ReplaceAllCopiesOf template)
        {
            cardToReplace = template.cardToReplace;
            cardToReplaceWith = template.cardToReplaceWith;
        }

        public override void PerformGameAction()
        {
            

            foreach(CardController card in Player.Instance.hand.cards)
            {
                if(card.cardData.name == cardToReplace)
                {
                    card.ClearFunctionality();
                    object[] rpcData = { cardToReplaceWith, true };
                    card.photonView.RPC("SetUpCardFromName", Photon.Pun.RpcTarget.All, rpcData);
                    Player.Instance.CheckIfCardCanBePlayed(card);
                }
            }

            Card replaceWith = CardLibrary.Instance.library[cardToReplaceWith];

            for (int i = 0; i < Player.Instance.GetDeck().cards.Count; i++)
            {
                if(Player.Instance.GetDeck().cards[i].name == cardToReplace)
                {
                    Player.Instance.GetDeck().cards[i] = new Card(replaceWith);
                }             
            }

            OnEnd();
        }
    }
}

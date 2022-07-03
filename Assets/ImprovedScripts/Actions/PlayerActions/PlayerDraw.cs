using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SequenceSystem;

namespace SequenceSystem
{
    public class PlayerDraw : GameAction
    {
        private Player player;
        private Card cardInfo;
        public GameAction overdrawAction;

        private List<TargetedAbility> abilities = new List<TargetedAbility>();
        public PlayerDraw(Player player) : base()
        {
            this.player = player;
            cardInfo = null;
            overdrawAction = new Lose(true);
        }

        public PlayerDraw(Player player, Card cardInfo)
        {
            this.player = player;
            this.cardInfo = cardInfo;
            overdrawAction = new Lose(true);
        }

        public PlayerDraw(PlayerDraw template)
        {
            player = template.player;
            cardInfo = template.cardInfo;
            // We need to reset the templates card
            template.cardInfo = null;
            overdrawAction = template.overdrawAction;

            abilities = new List<TargetedAbility>(template.abilities);
            template.abilities.Clear();
        }

        public override void PerformGameAction()
        {
            if (cardInfo == null)
            {
                cardInfo = player.GetDeck().DrawCard();
            }
            if (cardInfo != null)
            {
                CardController card = CardPool.Instance.Get();

                card.gameObject.SetActive(true);
                card.cardData = new Card(cardInfo);
                card.transform.position = player.GetDeck().transform.position;
                card.initialPos = player.GetDeck().transform.position;

                object[] rpcData = { cardInfo.GetJson(), false };
                card.photonView.RPC("SetUpCardFromJson", Photon.Pun.RpcTarget.All, rpcData);

                foreach (TargetedAbility ability in abilities)
                {
                    ability.SetTarget(card.gameObject);
                    MainSequenceManager.Instance.AddNext(ability);
                }
                // Get put in hand action
                MainSequenceManager.Instance.AddNext(card.putInHand);
            }
            else
            {
                MainSequenceManager.Instance.AddNext(overdrawAction);
            }

            cardInfo = null;


            OnEnd();
        }

        public void SetSpecificCard(Card card)
        {
            cardInfo = player.GetDeck().DrawSpecificCard(card);
        }

        public void AddAbilities(List<TargetedAbility> abilities)
        {
            this.abilities = new List<TargetedAbility>(abilities);
        }
    }
}

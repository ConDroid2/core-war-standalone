using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SequenceSystem;

namespace SequenceSystem
{
    public class PlayerMulligan : GameAction
    {
        private CardContainer mulliganZone;
        private PlayerDraw playerDraw;
        public int startingDraw = 3;

        private List<Card> cardsToMulligan = new List<Card>();
        private List<CardController> controllers = new List<CardController>();
        private List<int> cardsToGetRidOf = new List<int>();

        public PlayerMulligan(Player player)
        {
            mulliganZone = player.mulliganZone;
            playerDraw = player.draw;
        }

        public PlayerMulligan(PlayerMulligan template)
        {
            mulliganZone = template.mulliganZone;
            playerDraw = template.playerDraw;
            startingDraw = template.startingDraw;
        }

        public override void PerformGameAction()
        {
            CardController.OnSelected += HandleCardSelected;
            MultiUseButton.Instance.SetButtonFunction(DoneSelecting);
            MultiUseButton.Instance.SetButtonText("Done");
            MultiUseButton.Instance.SetInteractable(true);

            for (int i = 0; i < startingDraw; i++)
            {
                CardController card = GameObject.Instantiate(CardPool.Instance.cardPrefab);
                card.SetUpCardFromName(Player.Instance.GetDeck().cards[i].name);
                card.currentState = CardController.CardState.Default;
                mulliganZone.AddCard(card);
                cardsToMulligan.Add(Player.Instance.GetDeck().cards[i]);
                controllers.Add(card);
                card.potentialTarget.SetSelectable(true);
            }
        }

        public void HandleCardSelected(CardController card)
        {
            int cardIndex = controllers.IndexOf(card);

            if (cardsToGetRidOf.Contains(cardIndex))
            {
                // Debug.Log("Removing a get rid of card");
                cardsToGetRidOf.Remove(cardIndex);
                card.potentialTarget.SetSelected(false);
            }
            else
            {
                cardsToGetRidOf.Add(cardIndex);
                card.potentialTarget.SetSelected(true);
            }
        }

        public void DoneSelecting()
        {
            for (int i = 0; i < cardsToMulligan.Count; i++)
            {
                // If the card is not one we want to get rid of, draw it
                if (!cardsToGetRidOf.Contains(i))
                {
                    playerDraw.SetSpecificCard(cardsToMulligan[i]);
                    MainSequenceManager.Instance.AddNext(playerDraw);
                }
            }

            Player.Instance.GetDeck().Shuffle(10);

            for (int i = 0; i < cardsToGetRidOf.Count; i++)
            {
                MainSequenceManager.Instance.AddNext(playerDraw);
            }

            foreach (CardController card in controllers)
            {
                GameObject.Destroy(card.gameObject);
            }

            MultiUseButton.Instance.BackToDefault();
            MultiUseButton.Instance.SetInteractable(Player.Instance.myTurn);
            CardController.OnSelected -= HandleCardSelected;
            cardsToMulligan.Clear();
            cardsToGetRidOf.Clear();
            mulliganZone.gameObject.SetActive(false);

            OnEnd();
        }
    }
}

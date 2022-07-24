using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SequenceSystem 
{
    public class DiscardFromTopOfDeck : Ability
    {
        int amount;

        public delegate int GetAmount();
        private GetAmount getAmount;
        public DiscardFromTopOfDeck(int amount)
        {
            this.amount = amount;
            getAmount = DefaultGetAmount;
        }

        public DiscardFromTopOfDeck(GetAmount getAmount)
        {
            this.getAmount = getAmount;
        }

        public DiscardFromTopOfDeck(DiscardFromTopOfDeck template)
        {
            amount = template.amount;
            getAmount = template.getAmount;
            amount = getAmount();
        }

        public override void PerformGameAction()
        {
            Deck deck = Player.Instance.GetDeck();

            DiscardCard(deck);
        }

        private void DiscardCard(Deck deck)
        {
            if(amount == 0)
            {
                OnEnd();
            }
            else
            {
                CardController card = CardPool.Instance.Get();
                Card cardInfo = deck.DrawCard();

                if(cardInfo == null)
                {
                    MainSequenceManager.Instance.AddNext(new Lose(true));
                    OnEnd();
                }
                else
                {
                    card.gameObject.SetActive(true);
                    card.cardData = new Card(cardInfo);
                    card.transform.position = deck.transform.position;
                    card.initialPos = deck.transform.position;

                    card.SetUpCardFromJson(cardInfo.GetJson());

                    var sequence = DOTween.Sequence();

                    Tween move = card.transform.DOMove(Vector3.zero, 0.2f);

                    sequence.Append(move);
                    sequence.AppendInterval(0.2f);
                    sequence.AppendCallback(() =>
                    {
                        amount--;
                        MainSequenceManager.Instance.AddNext(card.discard);
                        card.gameObject.SetActive(false);
                        DiscardCard(deck);
                    });
                    sequence.Play();
                }
            }
        }

        public int DefaultGetAmount()
        {
            return amount;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SequenceSystem 
{
    public class ShuffleCardIntoDeck : TargetedAbility
    {
        public ShuffleCardIntoDeck()
        {
        }

        public ShuffleCardIntoDeck(ShuffleCardIntoDeck template) : base(template)
        {
        }

        public override void PerformGameAction()
        {
            
            CardController targetedCard = target.GetComponent<CardController>();
            Deck deck = Player.Instance.GetDeck();
            deck.AddCard(targetedCard.cardData.name);
            target.transform.DOMove(deck.transform.position, 0.3f).OnComplete(() => {
                
                MainSequenceManager.Instance.AddNext(targetedCard.removeFromPlay);
                OnEnd();
            });

            
        }
    }
}

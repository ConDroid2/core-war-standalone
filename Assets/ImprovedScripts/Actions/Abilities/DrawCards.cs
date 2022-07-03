using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    [System.Serializable]
    public class DrawCards : Ability
    {
        int amount;
        List<TargetedAbility> abilitiesTargetingCards = new List<TargetedAbility>();
        public DrawCards(int amount)
        {
            this.amount = amount;
        }

        public DrawCards(DrawCards template)
        {
            amount = template.amount;
            foreach(TargetedAbility ability in template.abilitiesTargetingCards)
            {
                abilitiesTargetingCards.Add(ability);
            }
        }

        public override void PerformGameAction()
        {
            Player player = Player.Instance;
            for (int i = 0; i < amount; i++)
            {
                Card cardInfo = player.GetDeck().DrawCard();

                if (cardInfo != null)
                {
                    CardController card = CardPool.Instance.Get();

                    card.gameObject.SetActive(true);
                    card.cardData = new Card(cardInfo);
                    card.transform.position = player.GetDeck().transform.position;
                    card.initialPos = player.GetDeck().transform.position;

                    object[] rpcData = { cardInfo.GetJson(), false };
                    card.photonView.RPC("SetUpCardFromJson", Photon.Pun.RpcTarget.All, rpcData);

                    foreach(TargetedAbility ability in abilitiesTargetingCards)
                    {
                        ability.SetTarget(card.gameObject);
                        MainSequenceManager.Instance.AddNext(ability);
                    }

                    // Get put in hand action
                    MainSequenceManager.Instance.AddNext(card.putInHand);
                }
                else
                {
                    MainSequenceManager.Instance.AddNext(new Lose(true));
                    // Stop drawing cards if we hit this
                    break;
                }
            }

            OnEnd();
        }

        public void AddAbility(TargetedAbility ability)
        {
            abilitiesTargetingCards.Add(ability);
        }
    }
}

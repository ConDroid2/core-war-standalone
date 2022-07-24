using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChitteringTide : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        ChitteringTideAction action = new ChitteringTideAction(card);

        onPlay.AddAbility(action);
    }

    public class ChitteringTideAction : SequenceSystem.Ability
    {
        CardController card;
        public ChitteringTideAction(CardController card)
        {
            this.card = card;
        }

        public ChitteringTideAction(ChitteringTideAction template)
        {
            card = template.card;
        }

        public override void PerformGameAction()
        {
            List<CardParent> sylkansInPlay = CardSelector.GetCards(zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit, subtypeFilter: "Sylkan");

            int amountInPlay = sylkansInPlay.Count;

            if (amountInPlay > 0)
            {
                Deck deck = Player.Instance.GetDeck();
                List<Card> cardsToPlay = new List<Card>();
                for (int i = 0; i < amountInPlay; i++)
                {
                    if (i < deck.cards.Count)
                    {
                        if (deck.cards[i].HasSubtype("Sylkan"))
                        {
                            cardsToPlay.Add(deck.cards[i]);
                        }
                    }
                }

                foreach (Card sylkan in cardsToPlay)
                {
                    deck.DrawSpecificCard(sylkan);

                    UnitController unit = InPlayCardPool.Instance.Get(card.transform.position);
                    unit.SetUpCardFromJson(sylkan.GetJson());
                    unit.gameObject.SetActive(true);

                    MainSequenceManager.Instance.Add(unit.unitAdvance);
                    MainSequenceManager.Instance.Add(unit.unitPlay);
                }
            }

            OnEnd();
        }
    }
}

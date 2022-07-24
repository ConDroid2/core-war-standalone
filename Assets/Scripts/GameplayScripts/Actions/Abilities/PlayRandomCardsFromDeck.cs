using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomCardsFromDeck : Ability
{
    private CardSelector.TypeFilter typeFilter;
    private int costFilter;
    private CardSelector.CostCompare costCompare;
    private string subtypeFilter;
    private string keywordFilter;
    private int cardsToPlay;

    public delegate bool OtherChecks(Card card);
    private OtherChecks otherChecks;


    public void Initialize(
        CardSelector.TypeFilter typeFilter = CardSelector.TypeFilter.All,
        int costFilter = -1,
        CardSelector.CostCompare costCompare = CardSelector.CostCompare.EqualTo,
        string subtypeFilter = "",
        string keywordFilter = "",
        OtherChecks otherChecks = null,
        int cardsToPlay = 1)
    {
        this.typeFilter = typeFilter;
        this.costFilter = costFilter;
        this.costCompare = costCompare;
        this.subtypeFilter = subtypeFilter;
        this.keywordFilter = keywordFilter;
        this.cardsToPlay = cardsToPlay;
        this.otherChecks = otherChecks;
    }

    public override IEnumerator ActionCoroutine()
    {
        List<Card> potentialCards = CardSelector.GetCardsFromDeck(typeFilter, costFilter, costCompare, subtypeFilter, keywordFilter);
        if(otherChecks != null)
        {
            potentialCards.RemoveAll((card) => !otherChecks(card));
        }

        List<Card> cards = new List<Card>();

        for (int i = 0; i < cardsToPlay; i++)
        {   
            if (potentialCards.Count > 0)
            {
                int randomIndex = Random.Range(0, potentialCards.Count);

                cards.Add(potentialCards[randomIndex]);
                potentialCards.RemoveAt(randomIndex);
            }
        }

        ActionSequencer sequence = new ActionSequencer();
        Deck deck = Player.Instance.GetDeck();

        foreach(Card card in cards)
        {
            deck.DrawSpecificCard(card);

            CardController cardController = CardPool.Instance.Get();

            cardController.gameObject.SetActive(true);
            cardController.cardData = card;
            cardController.transform.position = deck.transform.position;
            cardController.initialPos = deck.transform.position;

            cardController.SetUpCardFromName(card.fileName, false);

            sequence.AddGameAction(cardController.play);
            yield return StartCoroutine(sequence.RunSequence());
        }

        OnEnd();
    }
}

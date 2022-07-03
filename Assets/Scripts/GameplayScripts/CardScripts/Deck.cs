using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Deck : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public UnityEvent<int> OnCardAmountChanged;

    private void Awake() 
    {
        // Load the chosen deck
        if (PlayerPrefs.HasKey("ChosenDeck"))
        {             
            DeckData chosenDeckData = DeckSaveSystem.LoadDeck(PlayerPrefs.GetInt("ChosenDeck"));
            DeckObject chosenDeck = new DeckObject(chosenDeckData);

            if (chosenDeck != null)
            {
                for(int i = 0; i < chosenDeck.GetCardCount(); i++)
                {
                    cards.Add(new Card(chosenDeck.GetCardAt(i)));
                    OnCardAmountChanged?.Invoke(cards.Count);
                }
            }
        }

        Shuffle(20);
    }

    public void Shuffle() 
    {

        int cardsToShuffle = cards.Count;

        System.Random rng = new System.Random();
        while (cardsToShuffle > 0)
        {
            cardsToShuffle--;
            int differentCard = rng.Next(cardsToShuffle);
            Card temp = cards[differentCard];
            cards[differentCard] = cards[cardsToShuffle];
            cards[cardsToShuffle] = temp;
        }
    }

    public void Shuffle(int times)
    {
        for(int i = 0; i < times; i++)
        {
            Shuffle();
        }
    }

    public Card DrawCard() 
    {
        if (cards.Count > 0)
        {
            Card topCard = cards[0];

            cards.Remove(topCard);
            NetworkEventSender.Instance.SendEvent(new object[] { }, NetworkingUtilities.eventDictionary["EnemyRemoveCardFromDeck"]);

            OnCardAmountChanged?.Invoke(cards.Count);

            return topCard;
        } 
        else
        {
            return null;
        }
    }

    public Card DrawSpecificCard(Card card)
    {
        if(cards.Contains(card))
        {
            cards.Remove(card);
            NetworkEventSender.Instance.SendEvent(new object[] { }, NetworkingUtilities.eventDictionary["EnemyRemoveCardFromDeck"]);

            OnCardAmountChanged?.Invoke(cards.Count);
        }
        else
        {
            card = null;
        }

        return card;
    }

    public void AddCard(string cardName)
    {
        cards.Add(CardLibrary.Instance.library[cardName]);
        NetworkEventSender.Instance.SendEvent(new object[] { }, NetworkingUtilities.eventDictionary["EnemyAddCardToDeck"]);

        OnCardAmountChanged?.Invoke(cards.Count);
        Shuffle(10);
    }
}

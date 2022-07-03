using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckCreationManager : MonoBehaviour
{
    private Dictionary<Card, int> currentCards = new Dictionary<Card, int>();

    private DeckObject selectedDeck;
    private int selectedDeckSlot;

    [SerializeField] private Button saveButton;
    [SerializeField] private TMP_InputField deckNameField;

    public static event Action<Card> OnCardAmountIncreased;
    public static event Action<Card> OnCardAmountDecreased;
    public static event Action<Card> OnNewCardAdded;
    public static event Action<Card> OnCardRemoved;
    public static event Action SwitchToDeckScreen;

    private void Awake() 
    {
        CardSlot.OnCardClicked += AddCard;
        CardInDeckSlot.OnCardInDeckRemoved += RemoveCard;
        CardInDeckSlot.OnCardInDeckAdded += AddCard;
    }

    private void OnDestroy() 
    {
        CardSlot.OnCardClicked -= AddCard;
        CardInDeckSlot.OnCardInDeckRemoved -= RemoveCard;
        CardInDeckSlot.OnCardInDeckAdded -= AddCard;
    }


    public void SetUp(DeckObject deck, int slot) 
    {
        selectedDeckSlot = slot;
        selectedDeck = deck;

        for(int i = 0; i < selectedDeck.GetCardCount(); i++)
        {
            AddCard(selectedDeck.GetCardAt(i));
        }

        deckNameField.text = selectedDeck.GetName();
    }

    public void AddCard(Card card) 
    {
        if (GetTotalCards() < DeckUtilities.deckSize)
        {
            if (currentCards.ContainsKey(card))
            {
                if (currentCards[card] < card.amountAllowed || DevConfigs.IsDevBuild)
                {
                    currentCards[card]++;
                    OnCardAmountIncreased?.Invoke(card);
                }
            } 
            else
            {
                currentCards.Add(card, 1);
                OnCardAmountIncreased?.Invoke(card);
            }
        }
    }

    public void RemoveCard(Card card) 
    {
        if(currentCards[card] > 1)
        {
            currentCards[card]--;
            OnCardAmountDecreased?.Invoke(card);
        } 
        else
        {
            currentCards.Remove(card);
            OnCardAmountDecreased?.Invoke(card);
        }
    }

    public void SetDeckName(string newName) 
    {
        selectedDeck.SetName(newName);
        saveButton.interactable = !string.IsNullOrEmpty(selectedDeck.GetName());
    }

    public int GetTotalCards() 
    {
        int total = 0;
        foreach(KeyValuePair<Card, int> pair in currentCards)
        {
            total += pair.Value;
        }

        return total;
    }

    public void AttemptToSaveDeck() 
    {
        selectedDeck.ClearCards();

        Dictionary<Card, int>.KeyCollection cards = currentCards.Keys;
        foreach(Card card in cards)
        {
            for(int i = 0; i < currentCards[card]; i++)
            {
                selectedDeck.AddCard(card);
            }
        }

        DeckSaveSystem.SaveDeck(selectedDeck, selectedDeckSlot);
        currentCards.Clear();
        SwitchToDeckScreen?.Invoke();
    }

    public void Cancel() 
    {
        currentCards.Clear();

        SwitchToDeckScreen?.Invoke();
    }

    public void Delete() 
    {
        currentCards.Clear();
        selectedDeck.SetName("New Deck");
        AttemptToSaveDeck();

        SwitchToDeckScreen?.Invoke();
    }
}

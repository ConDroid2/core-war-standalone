using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardsInDeckManager : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform scrollContent;
    private List<CardInDeckSlot> slots = new List<CardInDeckSlot>();

    [SerializeField] private Vector2 startingPos;
    [SerializeField] private int yOffset;

    [SerializeField] private TextMeshProUGUI amountInDeck;

    private void Awake() 
    {
        DeckCreationManager.OnCardAmountIncreased += AddCard;
        DeckCreationManager.OnCardAmountDecreased += RemoveCard;
        DeckCreationManager.SwitchToDeckScreen += DeleteAll;
    }

    private void OnDestroy() 
    {
        DeckCreationManager.OnCardAmountIncreased -= AddCard;
        DeckCreationManager.OnCardAmountDecreased -= RemoveCard;
        DeckCreationManager.SwitchToDeckScreen -= DeleteAll;
    }


    public void Refresh() 
    {
        for(int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(startingPos.x, startingPos.y + (yOffset * i));
        }
    }


    public void AddCard(Card card) 
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].card == card)
            {
                slots[i].IncreaseAmount();
                CalcTotalCards();
                return;
            }
        }

        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(scrollContent, false);
        newSlot.GetComponent<CardInDeckSlot>().SetUp(card);
        slots.Add(newSlot.GetComponent<CardInDeckSlot>());

        CalcTotalCards();
        Refresh();
    }

    public void RemoveCard(Card card) 
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].card == card)
            {
                if (slots[i].amount > 1)
                {
                    slots[i].DecreaseAmount();
                    CalcTotalCards();
                } 
                else
                {
                    CardInDeckSlot slot = slots[i];
                    slots.Remove(slots[i]);
                    Destroy(slot.gameObject);
                    CalcTotalCards();
                    Refresh();
                    return;
                }
            }
        }
    }

    private void CalcTotalCards() 
    {
        int total = 0;

        foreach(CardInDeckSlot slot in slots)
        {
            total += slot.amount;
        }

        amountInDeck.text = total.ToString() + "/"+ DeckUtilities.deckSize.ToString();
    }

    public void DeleteAll() 
    {
        while(slots.Count > 0)
        {
            CardInDeckSlot slot = slots[0];
            slots.Remove(slots[0]);
            Destroy(slot.gameObject);
            amountInDeck.text = "0/" + DeckUtilities.deckSize.ToString();
        }
    }
}

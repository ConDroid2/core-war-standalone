using UnityEngine.EventSystems;
using System;
using UnityEngine;
using TMPro;

public class CardInDeckSlot : MonoBehaviour, IPointerClickHandler
{
    public Card card;
    [SerializeField] private TextMeshProUGUI cardNameTxt;
    [SerializeField] private TextMeshProUGUI amountTxt;
    [HideInInspector] public int amount = 1;

    private CardParent cardParent;

    public static event Action<Card> OnCardInDeckRemoved;
    public static event Action<Card> OnCardInDeckAdded;

    public void SetUp(Card newInfo) 
    {
        cardParent = GetComponent<CardParent>();
        card = newInfo;
        cardParent.cardData = card;

        cardNameTxt.text = card.name;
        amountTxt.text = amount.ToString();
    }

    public void IncreaseAmount() 
    {
        amount++;
        amountTxt.text = amount.ToString();
    }

    public void DecreaseAmount() 
    {
        amount--;
        amountTxt.text = amount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData) 
    {
        if (eventData.button != PointerEventData.InputButton.Right)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                OnCardInDeckAdded?.Invoke(card);
            }
            else
            {
                OnCardInDeckRemoved?.Invoke(card);
            }
        }
        else
        {
            cardParent.InvokeOnInspected();
        }
    }
}

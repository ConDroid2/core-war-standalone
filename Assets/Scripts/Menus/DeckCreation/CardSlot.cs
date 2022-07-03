using System;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class CardSlot : MonoBehaviour, IPointerClickHandler
{

    public Card card;
    private CardParent cardParent;
    private UICardGraphicsController graphicsController;

    public static event Action<Card> OnCardClicked;

    private void Awake() 
    {
        cardParent = GetComponent<CardParent>();
        graphicsController = GetComponent<UICardGraphicsController>();

        graphicsController.OnFilledIn += HandleFilledIn;
    }

    public void HandleFilledIn(Card card)
    {
        this.card = card;
        cardParent.cardData = card;
    }

    public void OnPointerClick(PointerEventData eventData) 
    {

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            cardParent.InvokeOnInspected();
        }
        else
        {
            OnCardClicked?.Invoke(card);
        }
        
    }
}

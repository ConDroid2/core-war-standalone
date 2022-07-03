using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class DeckSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int slotNumber;
    public DeckObject deck;
    public string deckName = "";
    [SerializeField] private TextMeshProUGUI deckText;

    [SerializeField] private Image image;
    [SerializeField] private Color validDeckColor;
    [SerializeField] private Color invalidDeckColor;

    public static event Action<int> OnDeckSelected;

    public void SetDeck(DeckObject newDeck) 
    {
        deck = newDeck;
        deckName = newDeck.GetName();
        deckText.text = deckName;

        if(deck.GetCardCount() < DeckUtilities.deckSize)
        {
            image.color = invalidDeckColor;
        }
        else
        {
            image.color = validDeckColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {

    }

    public void OnPointerExit(PointerEventData eventData) 
    {

    }

    public void OnPointerClick(PointerEventData eventData) 
    {
        if(deckName != "")
        {
            OnDeckSelected?.Invoke(slotNumber);
        }  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legendary : Keyword
{
    UnitController unit;
    string replacementCard = "";
    string thisCard = "";

    //private void Awake()
    //{
    //    unit = GetComponent<UnitController>();
    //    Debug.Log("Legendary awake");
    //    thisCard = unit.cardData.fileName;  
    //}

    public void Initialize(string replace)
    {
        unit = GetComponent<UnitController>();
        thisCard = unit.cardData.fileName;

        replacementCard = replace;

        if (unit.isMine)
        {
            unit.unitPlay.OnActionStart += ReplaceAllCards;
            unit.OnRemovedFromPlay += SwapCardsBack;

            Player.Instance.hand.OnCardAdded += ReplaceCard;
        }    
    }

    public void ReplaceAllCards()
    {
        foreach(CardParent card in Player.Instance.hand.cards)
        {
            ReplaceCard(card); 
        }
    }

    public void ReplaceCard(CardParent card)
    {
        if (card.cardData.fileName == thisCard)
        {
            CardController cardController = card as CardController;
            cardController.ClearFunctionality();
            cardController.SetUpCardFromName(replacementCard, true);
            Player.Instance.CheckIfCardCanBePlayed(cardController);
        }
    }

    public void SwapCardsBack()
    {
        Player.Instance.hand.OnCardAdded -= ReplaceCard;
        // Swap back cards in hand if you have no other instances of that legendary in play
        List<CardParent> otherInstances = CardSelector.GetSpecificCard(cardName: unit.cardData.name, zoneFilter: CardSelector.ZoneFilter.MyZones);
        otherInstances.Remove(unit);
        if(otherInstances.Count == 0)
        {
            foreach (CardParent card in Player.Instance.hand.cards)
            {
                if (card.cardData.fileName == replacementCard)
                {
                    // Debug.Log("Swapping card in hand back to " + thisCard);
                    CardController cardController = card as CardController;
                    cardController.ClearFunctionality();
                    cardController.SetUpCardFromName(thisCard, true);
                    Player.Instance.CheckIfCardCanBePlayed(cardController);
                }
            }
        }     

        unit.unitPlay.OnActionStart -= ReplaceAllCards;
        unit.OnRemovedFromPlay -= SwapCardsBack;
    }
}

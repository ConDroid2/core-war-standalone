using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// A script that should be on anything that contains cards in a physical, as in cards need to be placed down
public class CardContainer : MonoBehaviour 
{
    [HideInInspector] public List<CardParent> cards = new List<CardParent>();
    private List<float> xPositions = new List<float>();
    public int Count => cards.Count;
    [SerializeField] private int maxCardsBeforeReadjust;
    [SerializeField] private float cardDist = 0; // The distance between cards
    [SerializeField] private Vector3 cardZPosDif = new Vector3(0f, 0f, 0.01f);

    public Action<CardParent> OnCardAdded;
    public Action<CardParent> OnCardRemoved;
    
    /** Functional functions **/
    // AddCard function that will add the card to the list of and recalculate the card positions (and possibly their scale)
    public Vector3 AddCard(CardParent card) 
    {
        cards.Add(card);
        OnCardAdded?.Invoke(card);
        // card.OnHovered += CardHovered;
        // card.OnUnHovered += CardUnHovered;
        return ReconfigureCardPos(false);
    }

    public void RemoveCard(CardParent card) 
    {
        if (cards.Contains(card))
        {
            // card.OnHovered -= CardHovered;
            // card.OnUnHovered -= CardUnHovered;
            int index = cards.IndexOf(card);
            cards.Remove(card);
            // xPositions.RemoveAt(index);
            OnCardRemoved?.Invoke(card);
            ReconfigureCardPos(true);
        }
    }

    // Figure out where the cards should be placed based on how many cards there are
    public Vector3 ReconfigureCardPos(bool triggeredByRemove) 
    {
        xPositions.Clear();
        float adjustedCardDist = cardDist;
        if(maxCardsBeforeReadjust != 0 && cards.Count > maxCardsBeforeReadjust)
        {
            adjustedCardDist = cardDist * maxCardsBeforeReadjust / cards.Count;
        }
        //If there is only one card
        if (cards.Count == 1)
        {
            // If the card is going to move itself into the final position
            if(cards[0].GetComponent<InPlayCardController>() != null && !triggeredByRemove)
            {
                return transform.position - cardZPosDif;
            }
            // If the card needs to be moved
            else
            {
                cards[0].Move(transform.position - cardZPosDif);
            }
        }
        else
        {
            float firstXPos = 0f;

            // If there are an odd amount of cards
            if (!(cards.Count % 2 == 0))
            {
                firstXPos = transform.position.x - (adjustedCardDist * (cards.Count / 2));
            }
            // If there are an odd amount of cards
            else
            {
                float distanceFromMiddle = adjustedCardDist / 2;
                firstXPos = transform.position.x - distanceFromMiddle - (adjustedCardDist * ((cards.Count / 2) - 1));
            }

            int cardSortingLayer = 10;
            // Actually calulate the positions
            for (int i = 0; i < cards.Count; i++)
            {
                float newXPos = firstXPos + (adjustedCardDist * i);
                xPositions.Add(newXPos);
                if (i < cards.Count - 1)
                {
                    cards[i].Move(new Vector3(newXPos, transform.position.y, transform.position.z) - cardZPosDif);
                } 
                else
                {
                    if(cards[i].GetComponent<InPlayCardController>() != null && !triggeredByRemove)
                    {
                        return new Vector3(newXPos, transform.position.y, transform.position.z) - cardZPosDif;
                    } 
                    else
                    {
                        cards[i].Move(new Vector3(newXPos, transform.position.y, transform.position.z) - cardZPosDif);
                    }
                    
                }

                cards[i].sortingGroup.sortingOrder = cardSortingLayer;
                cards[i].sortingLayer = cardSortingLayer;
                cardSortingLayer--;
            }
        }

        return Vector3.one;
    }

    public void CardHovered(CardParent card)
    {
        if (!cards.Contains(card) || cards.Count < maxCardsBeforeReadjust || maxCardsBeforeReadjust == 0)
        {
            return;
        }

        int cardIndex = cards.IndexOf(card);
        float desiredXDist = card.transform.position.x - cardDist;

        if(cardIndex - 1 >= 0 && cardIndex < cards.Count)
        {
            desiredXDist = Mathf.Abs(desiredXDist - xPositions[cardIndex - 1]);
        }
        else
        {
            desiredXDist = Mathf.Abs(xPositions[cardIndex + 1] - desiredXDist);
        }

        float XPosOnLeft = xPositions[cardIndex] - (desiredXDist * cardIndex);

        for(int i = 0; i < cardIndex; i++)
        {
            CardParent temp = cards[i];
            temp.Move(new Vector3(XPosOnLeft, temp.transform.position.y, temp.transform.position.z));
            XPosOnLeft -= desiredXDist;
        }

        float XPosOnRight = xPositions[cardIndex] + desiredXDist;
        card.Move(new Vector3(xPositions[cardIndex], card.transform.position.y, card.transform.position.z));

        for(int i = cardIndex + 1; i < cards.Count; i++)
        {
            CardParent temp = cards[i];
            temp.Move(new Vector3(XPosOnRight, temp.transform.position.y, temp.transform.position.z));
            XPosOnRight += desiredXDist;
        }
    }

    public void CardUnHovered(CardParent card)
    {
        ReconfigureCardPos(false);
    }
}

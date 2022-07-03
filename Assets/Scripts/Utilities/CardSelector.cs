using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardSelector
{
    public enum HandFilter { MyHand, EnemyHand, All, None }
    public enum ZoneFilter { MyZones, EnemyZones, All, None }
    public enum TypeFilter { Unit, Support, Spell, All }
    public enum CostCompare { LessThanOrEqualTo, EqualTo, GreaterThanOrEqualTo }
    public enum OriginatingCard { TargetedAbility, Default };

    public static List<CardParent> GetCards(
        HandFilter handFilter = HandFilter.None, 
        ZoneFilter zoneFilter = ZoneFilter.None, 
        TypeFilter typeFilter = TypeFilter.All,
        int costFilter = -1,
        CostCompare costCompare = CostCompare.EqualTo,
        OriginatingCard originatingCard = OriginatingCard.Default,
        string subtypeFilter = "",
        string keywordFilter = "",
        string statusFilter = "")
    {
        List<CardParent> returnCards = new List<CardParent>();

        // Get all cards in the given hand
        if(handFilter != HandFilter.None)
        {
            if(handFilter == HandFilter.EnemyHand || handFilter == HandFilter.All)
            {
                foreach(CardParent card in Enemy.Instance.hand.cards)
                {
                    returnCards.Add(card);
                }
            }
            if(handFilter == HandFilter.MyHand || handFilter == HandFilter.All)
            {
                foreach(CardParent card in Player.Instance.hand.cards)
                {
                    returnCards.Add(card);
                }
            }
        }
        // Get all cards in the given zones
        if(zoneFilter != ZoneFilter.None)
        {
            if (zoneFilter == ZoneFilter.EnemyZones || zoneFilter == ZoneFilter.All)
            {
                foreach (Zone zone in Enemy.Instance.zones)
                {
                    foreach (CardParent card in zone.cards.cards)
                    {
                        returnCards.Add(card);
                    }
                }
            }

            if (zoneFilter == ZoneFilter.MyZones || zoneFilter == ZoneFilter.All)
            {
                foreach(Zone zone in Player.Instance.zones)
                {
                    foreach(CardParent card in zone.cards.cards)
                    {
                        returnCards.Add(card);
                    }    
                }
            }   
        }
        // Remove all cards that aren't a given type
        if(typeFilter != TypeFilter.All)
        {
            if(typeFilter == TypeFilter.Unit) { returnCards.RemoveAll(card => card.cardData.type != CardUtilities.Type.Character); }
            else if(typeFilter == TypeFilter.Support) { returnCards.RemoveAll(card => card.cardData.type != CardUtilities.Type.Support); }
            else if(typeFilter == TypeFilter.Spell) { returnCards.RemoveAll(card => card.cardData.type != CardUtilities.Type.Spell); }
        }
        
        // Check if there is a cost filter
        if(costFilter > -1)
        {
            // If there is, get rid of all that don't fit the cost compare
            if (costCompare == CostCompare.EqualTo) { returnCards.RemoveAll(card => card.cardData.GetTotalCost() != costFilter); }
            else if(costCompare == CostCompare.GreaterThanOrEqualTo) { returnCards.RemoveAll(card => card.cardData.GetTotalCost() < costFilter); }
            else if(costCompare == CostCompare.LessThanOrEqualTo) { returnCards.RemoveAll(card => card.cardData.GetTotalCost() > costFilter); }
        }

        if(originatingCard == OriginatingCard.TargetedAbility && handFilter == HandFilter.None)
        {
            returnCards.RemoveAll(card => card.cardData.keywords.Contains("Resistant"));
        }

        if(subtypeFilter != "")
        {
            returnCards.RemoveAll(card => (!card.cardData.HasSubtype(subtypeFilter)));
        }

        if(statusFilter != "")
        {
            returnCards.RemoveAll(card => (!card.cardData.activeStatuses.Contains(statusFilter)));
        }

        if(keywordFilter != "")
        {
            returnCards.RemoveAll(card => (!card.cardData.keywords.Contains(keywordFilter)));
        }

        return returnCards;
    }

    public static List<CardParent> GetSpecificCard(string cardName = "", ZoneFilter zoneFilter = ZoneFilter.None, HandFilter handFilter = HandFilter.None)
    {
        List<CardParent> returnCards = new List<CardParent>();

        if(handFilter != HandFilter.None)
        {
            foreach(CardParent card in Player.Instance.hand.cards)
            {
                if(card.cardData.fileName == cardName)
                {
                    returnCards.Add(card);
                }
            }
        }

        else if(zoneFilter != ZoneFilter.None)
        {
            List<Zone> zones = new List<Zone>();
            if(zoneFilter == ZoneFilter.MyZones || zoneFilter == ZoneFilter.All)
            {
                zones.Add(Player.Instance.zones[0]);
                zones.Add(Player.Instance.zones[1]);
            }
            if(zoneFilter == ZoneFilter.EnemyZones || zoneFilter == ZoneFilter.All)
            {
                zones.Add(Enemy.Instance.zones[0]);
                zones.Add(Enemy.Instance.zones[1]);
            }

            foreach (Zone zone in zones)
            {
                foreach (CardParent card in zone.cards.cards)
                {
                    if (card.cardData.fileName == cardName)
                    {
                        returnCards.Add(card);
                    }
                }
            }
        }

        return returnCards;
    }

    public static List<Card> GetCardsFromDeck(
        TypeFilter typeFilter = TypeFilter.All,
        int costFilter = -1,
        CostCompare costCompare = CostCompare.EqualTo,
        string subtypeFilter = "",
        string keywordFilter = ""
        )
    {
        List<Card> returnCards = new List<Card>();

        foreach(Card card in Player.Instance.GetDeck().cards)
        {
            bool fitsCriteria = true;

            if(typeFilter == TypeFilter.Unit && card.type != CardUtilities.Type.Character) { fitsCriteria = false; }
            if(typeFilter == TypeFilter.Support && card.type != CardUtilities.Type.Support) { fitsCriteria = false; }
            if(typeFilter == TypeFilter.Spell && card.type != CardUtilities.Type.Spell) { fitsCriteria = false; }

            if(costFilter > -1)
            {
                if(costCompare == CostCompare.EqualTo && card.GetTotalCost() != costFilter ) { fitsCriteria = false; }
                else if(costCompare == CostCompare.GreaterThanOrEqualTo && card.GetTotalCost() < costFilter) { fitsCriteria = false; }
                else if(costCompare == CostCompare.LessThanOrEqualTo && card.GetTotalCost() > costFilter) { fitsCriteria = false; }
            }

            if(subtypeFilter != "" && !card.HasSubtype(subtypeFilter)) { fitsCriteria = false; }
            if(keywordFilter != "" && !card.keywords.Contains(keywordFilter)) { fitsCriteria = false; }

            if (fitsCriteria)
            {
                returnCards.Add(card);
            }
        }

        return returnCards;
    }
}

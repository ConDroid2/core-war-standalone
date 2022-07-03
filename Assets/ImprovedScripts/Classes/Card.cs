using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Card
{
    public string Id;
    public string name;
    public string fileName;
    public CardUtilities.Type type;
    public string description;
    public string script = ""; // deprecate
    public Dictionary<string, int> cost;
    public Dictionary<string, int> defaultCost; // The cost of the card without any modifiers
    public int defaultResilience;
    public int maxResilience;
    public int currentResilience;
    public int armor = 0;
    public int defaultStrength;
    public int currentStrength;
    public int defaultInfluence;
    public int currentInfluence;
    public int amountAllowed;
    public bool allowedInDeck;
    public bool testOnly;
    public List<string> keywords;
    public List<string> subtypes;
    public string imagePath;
    public string flavorText;
    public string relatedCards;

    public List<string> statusImmunities;
    public List<string> activeStatuses;

    public enum PrimarySchool { Order, Creation, Chaos, Destruction, Neutral }
    public PrimarySchool primarySchool;

    public Card() { }

    public Card(Card card)
    {
        Id = card.Id;
        name = card.name;
        fileName = card.fileName;
        type = card.type;
        description = card.description;
        script = card.script;
        cost = new Dictionary<string, int>(card.cost);
        defaultCost = new Dictionary<string, int>(card.defaultCost);
        defaultResilience = card.defaultResilience;
        maxResilience = card.maxResilience;
        currentResilience = card.currentResilience;
        defaultStrength = card.defaultStrength;
        currentStrength = card.currentStrength;
        defaultInfluence = card.defaultInfluence;
        currentInfluence = card.currentInfluence;
        allowedInDeck = card.allowedInDeck;
        keywords = new List<string>(card.keywords);
        subtypes = new List<string>(card.subtypes);
        primarySchool = card.primarySchool;
        imagePath = card.imagePath;
        flavorText = card.flavorText;

        statusImmunities = new List<string>(card.statusImmunities);
        activeStatuses = new List<string>(card.activeStatuses);
    }

    public Card(CardJson json, bool fixStrings = false)
    {
        // Basic data
        if (fixStrings)
        {
            json.FixStrings();
        }      
        Id = json.cardId;
        name = json.name;
        fileName = json.name;
        description = json.description;
        script = json.script;
        defaultResilience = json.resistance.ConvertToInt();
        maxResilience = json.maxResilience.ConvertToInt();
        currentResilience = json.currentResilience.ConvertToInt();
        defaultStrength = json.attack.ConvertToInt();
        currentStrength = json.currentStrength.ConvertToInt();
        defaultInfluence = json.influence.ConvertToInt();
        currentInfluence = json.currentInfluence.ConvertToInt();
        keywords = json.GetKeywords();
        subtypes = json.GetSubtypes();
        allowedInDeck = json.allowed == 1; // if 1 true, else false
        amountAllowed = json.amountAllowed;
        imagePath = json.imagePath;
        flavorText = json.flavorText;
        relatedCards = json.relatedCards;

        // Cost data
        cost = new Dictionary<string, int>();
        cost.Add("Blue", json.costBlue);
        cost.Add("Red", json.costRed);
        cost.Add("Green", json.costGreen);
        cost.Add("Black", json.costBlack);
        cost.Add("Neutral", json.costNeutral);
        defaultCost = new Dictionary<string, int>(cost);

        // Type data
        if (json.type == "Spell") type = CardUtilities.Type.Spell;
        else if (json.type == "Unit") type = CardUtilities.Type.Character;
        else if (json.type == "Support") type = CardUtilities.Type.Support;
        else if (json.type == "Trap") type = CardUtilities.Type.Trap;
        else type = CardUtilities.Type.Error;

        // primary school data
        if (json.primarySchool == "Order") primarySchool = PrimarySchool.Order;
        else if (json.primarySchool == "Chaos") primarySchool = PrimarySchool.Chaos;
        else if (json.primarySchool == "Creation") primarySchool = PrimarySchool.Creation;
        else if (json.primarySchool == "Destruction") primarySchool = PrimarySchool.Destruction;
        else  primarySchool = PrimarySchool.Neutral;

        statusImmunities = new List<string>();
        activeStatuses = new List<string>();
    }

    public void PrintCardData()
    {
        string info = "";

    }

    public int GetTotalCost()
    {
        int totalCost = 0;

        totalCost += cost["Black"];
        totalCost += cost["Green"];
        totalCost += cost["Blue"];
        totalCost += cost["Red"];
        totalCost += cost["Neutral"];

        return totalCost;
    }

    public void TakeDamage(int damage)
    {
        currentResilience -= damage;
    }

    public bool HasSubtype(string subtype)
    {
        string[] subtypesToCheck = subtype.Split(',');

        foreach(string type in subtypesToCheck)
        {
            if(subtypes.Contains(type) || subtypes.Contains("All"))
            {
                return true;
            }
        }
        return false;
    }

    public string GetJson()
    {
        return JsonUtility.ToJson(new CardJson(this));
    }
}

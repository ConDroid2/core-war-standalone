using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardLibrary : MonoBehaviour
{
    [SerializeField] private TextAsset cardJSON;

    public bool CardsLoaded = false;

    public Dictionary<string, Card> library = new Dictionary<string, Card>();

    public Action<bool> OnLibraryDoneLoading;

    public static CardLibrary Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            LoadLibrary();
            DontDestroyOnLoad(this);   
        }
    }

    public void LoadLibrary()
    {
        CardJsonList cards = JsonUtility.FromJson<CardJsonList>(cardJSON.text);

        foreach (CardJson json in cards.cards)
        {
            library.Add(json.name, new Card(json, true));
        }

        OnLibraryDoneLoading?.Invoke(library.Count != 0);
    }
    
}


[Serializable]
public class CardJsonList
{
    public CardJson[] cards;
}

[Serializable]
public class CardJson
{
    public string cardId;
    public int costGreen;
    public int costBlue;
    public string keywords;
    public string imagePath;
    public int allowed;
    public string description;
    public int amountAllowed;
    public string type;
    public int costNeutral;
    public string resistance;
    public string maxResilience;
    public string currentResilience;
    public string script;
    public string subtypes;
    public string attack;
    public string currentStrength;
    public string influence;
    public string currentInfluence;
    public string name;
    public int costRed;
    public string primarySchool;
    public int costBlack;
    public string flavorText;
    public string relatedCards;

    public CardJson(Card card)
    {
        cardId = card.Id;
        name = card.name;
        description = card.description;
        script = card.script;
        resistance = card.defaultResilience.ToString();
        maxResilience = card.maxResilience.ToString();
        currentResilience = card.currentResilience.ToString();
        attack = card.defaultStrength.ToString();
        currentStrength = card.currentStrength.ToString();
        keywords = string.Join(",", card.keywords);
        subtypes = string.Join(",", card.subtypes);
        allowed = card.allowedInDeck ? 1 : 0;
        amountAllowed = card.amountAllowed;
        imagePath = card.imagePath;
        flavorText = card.flavorText;
        relatedCards = card.relatedCards;
        costBlack = card.cost["Black"];
        costBlue = card.cost["Blue"];
        costGreen = card.cost["Green"];
        costRed = card.cost["Red"];
        costNeutral = card.cost["Neutral"];
        influence = card.defaultInfluence.ToString();
        currentInfluence = card.currentInfluence.ToString();

        if (card.type == CardUtilities.Type.Character) type = "Unit";
        else if (card.type == CardUtilities.Type.Spell) type = "Spell";
        else if (card.type == CardUtilities.Type.Support) type = "Support";
        else if (card.type == CardUtilities.Type.Error) type = "";

        if (card.primarySchool == Card.PrimarySchool.Chaos) primarySchool = "Chaos";
        else if (card.primarySchool == Card.PrimarySchool.Creation) primarySchool = "Creation";
        else if (card.primarySchool == Card.PrimarySchool.Destruction) primarySchool = "Destruction";
        else if (card.primarySchool == Card.PrimarySchool.Neutral) primarySchool = "Neutral";
        else if (card.primarySchool == Card.PrimarySchool.Order) primarySchool = "Order";
    }

    public void FixStrings()
    {
        if (keywords == "none") keywords = "";
        if (imagePath == "none") imagePath = "";
        if (description == "none") description = "";
        if (type == "none") type = "";
        if (resistance == "none") resistance = "";
        if (script == "none") script = "";
        if (subtypes == "none") subtypes = "";
        if (attack == "none") attack = "";
        if (name == "none") name = "";
        if (primarySchool == "none") primarySchool = "";
        if (flavorText == "none") flavorText = "";
        if (relatedCards == "none") relatedCards = "";
        if (influence == "none") influence = "";

        maxResilience = resistance;
        currentResilience = resistance;
        currentStrength = attack;
        currentInfluence = influence;
    }

    public List<string> GetKeywords()
    {
        List<string> keywordList = new List<string>();

        if(keywords != "none")
        {
            string[] keywordArray;
            keywordArray = keywords.Split(',');

            foreach(string keyword in keywordArray)
            {
                keywordList.Add(keyword);
            }
        }

        return keywordList;
    }

    public List<string> GetSubtypes()
    {
        List<string> subtypeList = new List<string>();

        if (subtypes != "none")
        {
            string[] subtypeArray;
            subtypeArray = subtypes.Split(',');

            foreach (string subtype in subtypeArray)
            {
                subtypeList.Add(subtype);
            }
        }

        return subtypeList;
    }
}

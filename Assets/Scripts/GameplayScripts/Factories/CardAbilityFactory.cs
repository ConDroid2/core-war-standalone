using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using SequenceSystem;

public class CardAbilityFactory : MonoBehaviour
{
    public Dictionary<string, Type> cardScriptsByName;
    public Dictionary<string, Type> keywordsByName;
    public Dictionary<string, Type> inHandKeywordsByName;

    public static CardAbilityFactory Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            InitializeCardAbilityFactory();
        }
    }

    private void InitializeCardAbilityFactory() 
    {
        // Setting up ability dictionary
        var cardScripts = Assembly.GetAssembly(typeof(CardScript)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(CardScript)));
        cardScriptsByName = new Dictionary<string, Type>();

        foreach(var type in cardScripts)
        {
            cardScriptsByName.Add(type.Name, type);
        }

        // Setting up keyword dictionary
        var keywords = Assembly.GetAssembly(typeof(Keyword)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Keyword)));
        keywordsByName = new Dictionary<string, Type>();

        foreach (var type in keywords)
        {
            keywordsByName.Add(type.Name, type);
        }

        // Setting up in hand dictionary
        var inHandKeywords = Assembly.GetAssembly(typeof(InHandKeyword)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(InHandKeyword)));
        inHandKeywordsByName = new Dictionary<string, Type>();

        foreach (var type in inHandKeywords)
        {
            inHandKeywordsByName.Add(type.Name, type);
        }
    }

    public void AddCardFunctionality(CardParent card) 
    {
        // This secion adds abilities based on keywords
        if (card.cardData.keywords.Count > 0)
        {
            foreach (string keyword in card.cardData.keywords)
            {
                AddKeyword(keyword, card);
            }
        }

        if (cardScriptsByName.ContainsKey(card.cardData.script))
        {
            card.gameObject.AddComponent(cardScriptsByName[card.cardData.script]);
        }
    }

    public void AddInHandCardFunctionality(CardParent card)
    {
        if (card.cardData.keywords.Count > 0)
        {
            foreach (string keyword in card.cardData.keywords)
            {
                AddInHandKeyword(keyword, card);
            }
        }

        if (cardScriptsByName.ContainsKey(card.cardData.script))
        {
            CardScript script = card.gameObject.AddComponent(cardScriptsByName[card.cardData.script]) as CardScript;
            script.InHandSetUp();
        }
    }

    public void AddInPlayCardFunctionality(CardParent card)
    {
        if (card.cardData.keywords.Count > 0)
        {
            foreach (string keyword in card.cardData.keywords)
            {
                AddKeyword(keyword, card);
            }
        }

        if (cardScriptsByName.ContainsKey(card.cardData.script))
        {
            CardScript script = card.gameObject.AddComponent(cardScriptsByName[card.cardData.script]) as CardScript;
            script.InPlaySetUp();
        }
    }

    public void AddKeyword(string keywordName, CardParent card)
    {
        if (keywordsByName.ContainsKey(keywordName))
        {
            Type keywordType = keywordsByName[keywordName];
            card.gameObject.AddComponent(keywordType);
        }
    }

    public void RemoveKeyword(string keywordName, CardParent card)
    {
        if (keywordsByName.ContainsKey(keywordName))
        {
            Type keywordType = keywordsByName[keywordName];
            Keyword keyword = card.gameObject.GetComponent(keywordType) as Keyword;
            keyword.RemoveKeyword();
        }
        if (card.cardData.keywords.Contains(keywordName))
        {
            card.cardData.keywords.Remove(keywordName);
        }
    }

    public void AddInHandKeyword(string keywordName, CardParent card)
    {
        if (inHandKeywordsByName.ContainsKey(keywordName))
        {
            Type keywordType = inHandKeywordsByName[keywordName];
            card.gameObject.AddComponent(keywordType);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntInput
{
    public delegate int GetValueFunction(CardParent cardParent);
    private string valueKey;
    private CardParent cardParent;

    public int Value => GetValue();

    private Dictionary<string, GetValueFunction> valueDictionary = new Dictionary<string, GetValueFunction>
    {
        {"MyStrength", (cardParent) => cardParent.cardData.currentStrength },
        {"MyResilience", (cardParent) => cardParent.cardData.currentResilience },
        {"IgniteCount", (cardParent) => IgniteManager.Instance.igniteCount}
    };

    public IntInput(string valueKey, CardParent cardParent)
    {
        this.valueKey = valueKey;
        this.cardParent = cardParent;
    }

    public int GetValue()
    {
        int value = 0;

        // If the value is not a number, call the function
        if(!int.TryParse(valueKey, out value))
        {
            if (valueDictionary.ContainsKey(valueKey))
            {
                value = valueDictionary[valueKey](cardParent);
            }
        }

        return value;
    }
}

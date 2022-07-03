using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class CardBaseObjectUtilities
{
    public static CardBaseObject GetCardByFile(string fileName) 
    {
        // string[] cardGUIDs = AssetDatabase.FindAssets(fileName, new[] { "Assets/Cards" });
        CardBaseObject card;
        if (card = Resources.Load<CardBaseObject>("Cards/" + fileName))
        {
            return card;
        } 
        else
        {
            return null;
        }
    }
}

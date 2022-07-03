using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtensionMethods
{
    public static int ConvertToInt(this string str) 
    {
        return int.Parse(str);
    }

    public static Card ConvertToCard(this string str) 
    {
        return CardLibrary.Instance.library[str.Trim()];
    }
}

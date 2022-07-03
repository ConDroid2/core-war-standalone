using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardUtilities
{
    public enum Keywords {Unlimited, Fierce, Haste, Steady, Error};
    public enum EnergyColor {Red, Blue, Black, Green, Neutral, Error};
    public enum Type {Spell, Support, Character, Trap, Error};

    [System.Serializable]
    public struct Keyword
    {
        public Keywords word;
        public int number;
    }

    [System.Serializable]
    public struct Energy 
    {
        public EnergyColor color;
        public int amount;
    }

    public static Type GetType(string type) 
    {

        Type actualType;

        if(System.Enum.TryParse<Type>(type, out actualType))
        {
            return actualType;
        }
        else
        {
            return Type.Error;
        } 
    }

    public static Keyword GetKeyword(string[] values) 
    {
        Keywords actualKeyword;
        Keyword keyword = new Keyword();

        if (System.Enum.TryParse<Keywords>(values[0], out actualKeyword)) { } 
        else { actualKeyword = Keywords.Error; }

        
        keyword.word = actualKeyword;

        if(values.Length == 2)
        {
            keyword.number = values[1].ConvertToInt();
        }
        else
        {
            keyword.number = 0;
        }
        

        return keyword;  
    }

    public static EnergyColor GetEnergyColor(string color) 
    {
        EnergyColor energyColor;

        if (System.Enum.TryParse<EnergyColor>(color, out energyColor))
        {
            return energyColor;
        } 
        else
        {
            return EnergyColor.Error;
        }
    }

    public static bool ContainsKeyword(List<Keyword> list, Keywords keyword) 
    {
        foreach(Keyword word in list)
        {
            if (word.word == keyword)
            {
                return true;
            }
        }

        return false;
    }
}

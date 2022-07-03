using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/Card")]
public class CardBaseObject : ScriptableObject
{
    public CardUtilities.Type type;
    public string cardName = "";
    public string description = "";
    public string script = "";
    public int blueCost = 0;
    public int redCost = 0;
    public int greenCost = 0;
    public int blackCost = 0;
    public int neutralCost = 0;
    public int resilience = 0;
    public int damage = 0;
    public bool allowedInDeck = true;
    public int amountAllowed = 4;

    // List of keywords
    public List<string> keywords = new List<string>();

    // List of Subtypes
    public List<string> subtypes = new List<string>();

    public Card.PrimarySchool primarySchool = Card.PrimarySchool.Neutral;

    public bool HasSubtype(string subtype)
    {
        return (subtypes.Contains(subtype) || subtypes.Contains("All"));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RuneUtilities
{
    public static Dictionary<string, string> runeDescriptions = new Dictionary<string, string>()
    {
        {"Tutor", "When I enters a zone, all units with lower Strength or Resilience than me get +1/+1" },
        {"Prophecy", "When paying for this card, you may Prophesy it, the card will then be played in X turns where X is the amount of Neutral cost you didn't pay for" },
        {"Resistant", "I cannot be targeted by abilities" },
        {"Icetouch", "When this card deals damage to a card, that card is Frozen for 2 turns" },
        {"Regeneration", "I fully heal at the end of every turn" },
        {"Immobile", "This unit cannot advance" },
        {"Legendary", "While I am in play, all other copies in your hand will become a different card" },
        {"Swift", "I can act on the turn it was played" },
        {"Defender", "While this card is in play, your opponent may not enter the core" },
        {"Haunt", "My abilities function while in play and in the Underworld" }
    };
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetworkingUtilities
{
    public static Dictionary<string, byte> eventDictionary = new Dictionary<string, byte>{
        { "StartAsFirst", 0 },
        { "TurnEnded" , 1 },
        { "EnemyWon", 2 },
        { "EnemyEnergyChange", 3 },
        { "EnemyLost", 4 },
        { "StartAsSecond", 5 },
        { "DoneWaitingForOpponent", 6 },
        { "EnemyAddCardToDeck", 7 },
        { "EnemyRemoveCardFromDeck", 8 },
        { "EnemyCardEnteredCore", 9 }
    };
}

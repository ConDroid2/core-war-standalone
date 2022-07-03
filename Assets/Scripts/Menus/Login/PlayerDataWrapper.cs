using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[Serializable]
public class PlayerData
{
    public string playerName;
    public PlayerInfo value;
}

[Serializable]
public class PlayerInfo
{
    public string test;
    public DeckData[] decks;
}

public class PlayerList
{
    public PlayerData[] players;
}

public class PlayerDataWrapper : MonoBehaviour
{
    public PlayerData player;

    public Action<bool> OnPlayerLoaded;

    public static PlayerDataWrapper Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}

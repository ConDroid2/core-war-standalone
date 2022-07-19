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
    public DeckData[] decks;
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

            string path = Application.persistentDataPath + "/playerData.data";

            if (System.IO.File.Exists(path))
            {
                string data = System.IO.File.ReadAllText(path);
                player = JsonUtility.FromJson<PlayerData>(data);

                Debug.Log("Loaded player: " + data);
            }
            else
            {
                PlayerData newPlayer = new PlayerData();
                newPlayer.playerName = "Player";
                newPlayer.decks = new DeckData[0];

                string data = JsonUtility.ToJson(newPlayer);
                System.IO.File.WriteAllText(path, data);

                Debug.Log("Created new Player: " + data);
            }
        }
    }
}

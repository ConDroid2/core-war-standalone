using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DeckSaveSystem
{

    public static void SaveDeck(DeckObject deck, int slot) 
    {
        DeckData deckData = new DeckData(deck);

        if (slot < PlayerDataWrapper.Instance.player.value.decks.Length)
        {
            PlayerDataWrapper.Instance.player.value.decks[slot] = deckData;
        }
        else
        {
            Array.Resize(ref PlayerDataWrapper.Instance.player.value.decks, PlayerDataWrapper.Instance.player.value.decks.Length + 1);
            PlayerDataWrapper.Instance.player.value.decks[slot] = deckData;
        }



        string data = JsonUtility.ToJson(PlayerDataWrapper.Instance.player);
        Debug.Log("Player Json: ");
        Debug.Log(data);

        string path = Application.persistentDataPath + "/playerData.data";

        if (File.Exists(path))
        {
           // Debug.Log("Saving Decks");
            File.WriteAllText(path, data);
        }
    }

    public static DeckData LoadDeck(int slot) 
    {
        if (PlayerDataWrapper.Instance.player.value.decks.Length > slot)
        {
            return PlayerDataWrapper.Instance.player.value.decks[slot];
        }

        else
        {
            return null;
        }
    }

    public static List<DeckData> LoadAllDecks() 
    {
        List<DeckData> deckDatas = new List<DeckData>();

        for(int i = 0; i < 8; i++)
        {
            DeckData deck = LoadDeck(i);
            if(deck != null)
            {
                deckDatas.Add(deck);
            }
        }

        for(int i = 0; i < 8; i++)
        {
            if(i < deckDatas.Count)
            {
                // SaveDeck(new DeckObject(deckDatas[i]), i);
            }
            else
            {
                DeleteDeck(i);
            }
        }

        return deckDatas;
    }

    public static void DeleteDeck(int slot) 
    {
        string path = Application.persistentDataPath + "/deck" + slot + ".deck";
        if (File.Exists(path))
        {
            Debug.Log("Deleting slot " + slot);
            File.Delete(path);
        }
    }
}

using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class NetworkEventReceiver : MonoBehaviour
{
    // A default event that will be fired for different purposes
    public static Action OnNetworkEvent;

    public void SetUp()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived;
    }



    private void OnEventReceived(EventData eventData)
    {
        if (eventData.Code == NetworkingUtilities.eventDictionary["StartAsFirst"])
        {
            OnStartAsFirst();
        }

        else if(eventData.Code == NetworkingUtilities.eventDictionary["StartAsSecond"])
        {
            OnStartAsSecond();
        }

        else if (eventData.Code == NetworkingUtilities.eventDictionary["TurnEnded"])
        {
            OnTurnEnded();
        }

        else if (eventData.Code == NetworkingUtilities.eventDictionary["EnemyEnergyChange"])
        {
            object[] eventContent = (object[])eventData.CustomData;
            OnEnemyEnergyChange(eventContent[0].ToString(), eventContent[1].ToString().ConvertToInt(), eventContent[2].ToString().ConvertToInt());
        }

        else if(eventData.Code == NetworkingUtilities.eventDictionary["EnemyWon"])
        {
            MatchManager.Instance.YouLose(false);
            // MainSequenceManager.Instance.AddNext(new SequenceSystem.Lose(false));
        }

        else if(eventData.Code == NetworkingUtilities.eventDictionary["EnemyLost"])
        {
            MatchManager.Instance.YouWin(false);
            // MainSequenceManager.Instance.AddNext(new SequenceSystem.Win(false));
        }

        else if(eventData.Code == NetworkingUtilities.eventDictionary["DoneWaitingForOpponent"])
        {
            SendNetworkEventReceived();
        }

        else if(eventData.Code == NetworkingUtilities.eventDictionary["EnemyAddCardToDeck"])
        {
            Enemy.Instance.deckSize++;
        }

        else if(eventData.Code == NetworkingUtilities.eventDictionary["EnemyRemoveCardFromDeck"])
        {
            Enemy.Instance.deckSize--;
        }

        else if(eventData.Code == NetworkingUtilities.eventDictionary["EnemyCardEnteredCore"])
        {
            object[] eventContent = (object[])eventData.CustomData;
            OnEnemyCardEnteredCore((int)eventContent[0]);
        }
    }

    private void OnStartAsFirst()
    {
        GameManager.Instance.StartAsFirstPlayer();
    }

    private void OnStartAsSecond()
    {
        GameManager.Instance.StartAsSecondPlayer();
    }

    private void OnTurnEnded()
    {
        Player.Instance.StartTurn();     
    }

    private void OnEnemyEnergyChange(string color, int totalAmount, int currentAmount)
    {
        EnemyInfoManager.Instance.SetTotalEnergy(color, totalAmount, currentAmount);
    }

    private void SendNetworkEventReceived()
    {
        OnNetworkEvent?.Invoke();
    }

    private void OnEnemyCardEnteredCore(int influenceAmount)
    {
        Core.Instance.ChangeInfluence(-1 * influenceAmount, false);
    }
}

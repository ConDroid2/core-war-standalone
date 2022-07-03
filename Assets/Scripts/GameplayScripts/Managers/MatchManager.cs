using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MatchManager : MonoBehaviour
{
    public static MatchManager Instance;

    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject DefeatScreen;

    public int currentTurn;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        WinScreen.SetActive(false);
        DefeatScreen.SetActive(false);

        currentTurn = 1;
        Player.Instance.OnEndTurn += AdvanceTurn;
    }
    

    public void YouWin(bool sendEvent)
    {
        WinScreen.SetActive(true);

        if (sendEvent)
        {
            object[] eventContent = { };
            NetworkEventSender.Instance.SendEvent(eventContent, NetworkingUtilities.eventDictionary["EnemyWon"]);
            
        }
        if (PhotonNetwork.IsConnected  && PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public void YouLose(bool sendEvent)
    {
        DefeatScreen.SetActive(true);

        if (sendEvent)
        {
            object[] eventContent = { };
            NetworkEventSender.Instance.SendEvent(eventContent, NetworkingUtilities.eventDictionary["EnemyLost"]);     
        }
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public void AdvanceTurn()
    {
        currentTurn++;
    }
}

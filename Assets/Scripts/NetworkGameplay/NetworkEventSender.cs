using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class NetworkEventSender : MonoBehaviour
{
    public static NetworkEventSender Instance { get; private set; }
    public const byte TestEvent = 0;
    public const byte CharacterPlayed = 1;

    public void SetUp() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void SendEvent(object[] eventContent, byte eventCode) 
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent(eventCode, eventContent, raiseEventOptions, SendOptions.SendReliable);
    }
}

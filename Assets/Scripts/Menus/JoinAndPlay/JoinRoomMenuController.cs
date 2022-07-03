using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class JoinRoomMenuController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject PlayerNamePanel;
    [SerializeField] private GameObject JoinRoomPanel;
    [SerializeField] private GameObject Searching;
    [SerializeField] private TextMeshProUGUI searchingText;

    private bool isConnecting = false;

    private const string GameVersion = "0.01";
    private const int MaxPlayersPerRoom = 2;

    private void Awake() 
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void FindOpponent() 
    {
        isConnecting = true;

        JoinRoomPanel.SetActive(false);
        Searching.SetActive(true);
        searchingText.text = "Finding Opponent . . .";
        PhotonNetwork.NickName = PlayerDataWrapper.Instance.player.playerName;

        //if (PhotonNetwork.IsConnected)
        //{
        //    Debug.Log("Already connected, joining random room");
        //    PhotonNetwork.JoinLobby();
        //}
        //else
        {
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster() 
    {
        Debug.Log("Connected to master");

        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    //public override void OnJoinedLobby()
    //{
    //    PhotonNetwork.JoinRandomRoom();
    //}

    public override void OnDisconnected(DisconnectCause cause) 
    {
        Searching.SetActive(false);
        JoinRoomPanel.SetActive(true);

        // Debug.Log($"Disconnected due to: {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message) 
    {
        // Debug.Log("No clients are waiting for an opponent, creating a new room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });

    }

    public override void OnJoinedRoom() 
    {
        // Debug.Log("Client joined room: " + PhotonNetwork.CurrentRoom.PlayerCount);

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if(playerCount != MaxPlayersPerRoom)
        {
            searchingText.text = "Waiting for opponent";
        } 
        else
        {
            // Debug.Log("Match is ready to begin");
            searchingText.text = "Opponent Found";
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer) 
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            // Debug.Log("Match is read to begin in room: " + PhotonNetwork.CurrentRoom.Name);

            searchingText.text = "Opponent Found";

            PhotonNetwork.LoadLevel("CardGame");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviour
{   
    public Player currentPlayer;
    [SerializeField] private MainSequenceManager sequenceManager;
    [SerializeField] private NetworkEventSender eventSender;
    [SerializeField] private NetworkEventReceiver eventReciever;
    [SerializeField] private EnergyUIManager uiManager;
    [SerializeField] private OverlayUIManager overlayUI;
    [SerializeField] private MagickManager magickManager;

    public static GameManager Instance {get; private set;}

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }

        eventSender.SetUp();
        eventReciever.SetUp();
        sequenceManager.SetUp();
        magickManager.SetUp();
        currentPlayer.SetUp();
        uiManager.SetUp();
        overlayUI.SetUp();

        

        StartGame();

        // if(SceneManager.GetSceneByName("CardInspectorUI"))
        {
            SceneManager.LoadScene("CardInspectorUI", LoadSceneMode.Additive);
        }

    }

    public void StartGame() 
    {
        

        if (PhotonNetwork.IsMasterClient)
        {
            System.Random rng = new System.Random();

            int firstPlayer = rng.Next(99);

            object[] eventContent = { };

            if (firstPlayer % 2 == 0)
            {
                Player.Instance.myTurn = true;

                NetworkEventSender.Instance.SendEvent(eventContent, NetworkingUtilities.eventDictionary["StartAsSecond"]);
                StartAsFirstPlayer();
            } 
            else
            {
                Player.Instance.myTurn = false;
                
                NetworkEventSender.Instance.SendEvent(eventContent, NetworkingUtilities.eventDictionary["StartAsFirst"]);
                StartAsSecondPlayer();
            }
        }
    }

    public void StartAsSecondPlayer() 
    {
        Player.Instance.mulligan.startingDraw = 4;
        MainSequenceManager.Instance.Add(Player.Instance.mulligan);
    }

    public void StartAsFirstPlayer()
    {
       MainSequenceManager.Instance.Add(Player.Instance.mulligan);
       Player.Instance.StartTurn();
    }
}

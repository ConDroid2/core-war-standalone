using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LoginManager : MonoBehaviour
{
    private readonly string getUrl = "https://eytluyssud.execute-api.us-east-1.amazonaws.com/dev/";

    string username = "";

    private bool cardsLoaded = false;
    private bool playerLoaded = false;

    [SerializeField]
    private GameObject DebugPanel;
    [SerializeField]
    private TextMeshProUGUI debugText;

    public UnityEvent LoadingPlayer;
    public UnityEvent DoneLoadingPlayer;
    public UnityEvent OnAccountCreated;
    public void SetUsername(string name)
    {
        username = name;
    }

    public void HandlePlayerLoaded(bool loaded)
    {
        if(loaded == false)
        {
            DebugPanel.SetActive(true);
            debugText.text = "Couldn't find player";
        }
    }

    public void HandleCardsLoaded(bool loaded)
    {
        CardLibrary.Instance.OnLibraryDoneLoading -= HandleCardsLoaded;
        DoneLoadingPlayer?.Invoke();
        if (loaded == false)
        {
            DebugPanel.SetActive(true);
            debugText.text = "Something went wrong loading the cards, please try logging in again, or restarting the client";
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void AttemptLogin()
    {
        StartCoroutine(GetPlayerData(username));
    }

    public void Login()
    {
        if (PlayerDataWrapper.Instance.player == null)
        {
            Debug.Log("Player doesn't exist");
            HandlePlayerLoaded(false);
            DoneLoadingPlayer?.Invoke();
        }

        else
        {
            HandlePlayerLoaded(true);
            CardLibrary.Instance.OnLibraryDoneLoading += HandleCardsLoaded;
            CardLibrary.Instance.LoadLibrary();
        }
    }

    IEnumerator GetPlayerData(string username)
    {
        LoadingPlayer?.Invoke();
        UnityWebRequest playerRequest = UnityWebRequest.Get(getUrl);

        yield return playerRequest.SendWebRequest();

        if (playerRequest.isNetworkError || playerRequest.isHttpError)
        {
            Debug.Log("Error getting players");
        }

        else
        {
            PlayerList players = JsonUtility.FromJson<PlayerList>(playerRequest.downloadHandler.text);

            PlayerDataWrapper.Instance.player = null;

            foreach (PlayerData player in players.players)
            {
                if (player.playerName == username)
                {
                    if (player.value.decks == null)
                    {
                        player.value.decks = new DeckData[0];
                    }

                    PlayerDataWrapper.Instance.player = player;
                }
            }

            Login();
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using TMPro;

public class UserCreationManager : MonoBehaviour
{
    private readonly string putUrl = "https://eytluyssud.execute-api.us-east-1.amazonaws.com/dev/";

    public string newUsername = "";

    public GameObject DebugPanel;
    public TextMeshProUGUI debugText;

    public UnityEvent OnStartUserCreated;
    public UnityEvent OnUserCreated;

    public void SetNewUsername(string name)
    {
        newUsername = name;
    }

    public void CreateNewUser()
    {
        if (newUsername != "")
        {
            PlayerData newPlayer = new PlayerData();

            newPlayer.playerName = newUsername;

            PlayerInfo info = new PlayerInfo();
            info.test = "Test2";

            newPlayer.value = info;

            StartCoroutine(CreateAccount(newPlayer));
        }
    }

    IEnumerator CreateAccount(PlayerData newPlayer)
    {
        OnStartUserCreated?.Invoke();
        // byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(newPlayer));
        string data = JsonUtility.ToJson(newPlayer);
        UnityWebRequest putRequest = UnityWebRequest.Put(putUrl, data);

        yield return putRequest.SendWebRequest();

        if(putRequest.isHttpError || putRequest.isNetworkError)
        {
            DebugPanel.SetActive(true);
            debugText.text = "Something went wrong while creating your user, please try again";
        }
        else
        {
            OnUserCreated?.Invoke();
        }
    }
}

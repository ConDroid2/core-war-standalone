using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button setNameButton;
    [SerializeField] private GameObject thisPanel;
    [SerializeField] private GameObject findOpponentPanel;
    
    private const string PlayerPrefsNameKey = "playerName";

    private void Awake() 
    {
        SetUpInputField();
    }

    private void SetUpInputField() 
    {
        if(!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }

        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        inputField.text = defaultName;

        SetPlayerName(defaultName);
    }

    public void SetPlayerName(string name) 
    {
        setNameButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void SavePlayerName() 
    {
        string playerName = inputField.text;

        PhotonNetwork.NickName = playerName;

        PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);

        thisPanel.SetActive(false);
        findOpponentPanel.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private bool pausingAllowed = true;

    private void Update() 
    {
        if (pausingAllowed && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        }
    }

    public void LeaveGame() 
    {
        if (PhotonNetwork.IsConnected)
        {
            pauseMenu.SetActive(false);
            pausingAllowed = false;
            MatchManager.Instance.YouLose(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverlayUIManager : MonoBehaviour
{
    [SerializeField] private Button endTurn;

    // Start is called before the first frame update
    public void SetUp()
    {
        Player.Instance.OnEndTurn += HandleEndTurn;
        Player.Instance.OnStartTurn += HandleStartTurn;
    }

    private void OnDestroy() 
    {
        Player.Instance.OnEndTurn -= HandleEndTurn;
        Player.Instance.OnStartTurn -= HandleStartTurn;
    }

    private void HandleEndTurn() 
    {
        endTurn.interactable = false;
    }

    private void HandleStartTurn() 
    {
        endTurn.interactable = true;
    }

    public void ClickMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

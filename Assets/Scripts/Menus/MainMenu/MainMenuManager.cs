using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnPlay() 
    {
        SceneManager.LoadScene("DeckSelection");
    }

    public void OnEditDeck() 
    {
        SceneManager.LoadScene("DeckEditing");
    }
}

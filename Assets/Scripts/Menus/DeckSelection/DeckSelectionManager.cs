using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckSelectionManager : MonoBehaviour
{
    [SerializeField] private List<DeckSlot> deckSlots = new List<DeckSlot>();
    private List<DeckObject> decks;
    public string sceneToLoad = "CardGame";

    private void Awake() 
    {
        List<DeckData> decks = DeckSaveSystem.LoadAllDecks();

        int nextSlot = 0;

        for(int i = 0; i < decks.Count; i++)
        {
            DeckObject deck = new DeckObject(decks[i]);
            deckSlots[nextSlot].SetDeck(deck);
            deckSlots[nextSlot].gameObject.SetActive(true);
            nextSlot++;
        }

        DeckSlot.OnDeckSelected += HandleDeckSelected;
    }

    private void OnDestroy() 
    {
        DeckSlot.OnDeckSelected -= HandleDeckSelected;
    }

    private void HandleDeckSelected(int slot) 
    {
        if(deckSlots[slot].deck.GetCardCount() == DeckUtilities.deckSize)
        {
            Debug.Log("Deck selected");
            PlayerPrefs.SetInt("ChosenDeck", slot);
            SceneManager.LoadScene(sceneToLoad);
        }    
    }

    public void ReturnToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckEditingManager : MonoBehaviour
{

    [SerializeField] private GameObject decksUI;
    [SerializeField] private GameObject cardsUI;

    private DeckCreationManager deckManager;

    [SerializeField] private List<DeckSlot> deckSlots = new List<DeckSlot>();
    private List<DeckObject> decks = new List<DeckObject>();

    private void Awake() 
    {
        deckManager = cardsUI.GetComponent<DeckCreationManager>();

        // This is a dumb workaround, maybe look into issue later
        cardsUI.SetActive(true);
        cardsUI.SetActive(false);

        Refresh();

        DeckCreationManager.SwitchToDeckScreen += HandleDoneWithDeck;
        DeckSlot.OnDeckSelected += HandleDeckSelection;
        SceneManager.LoadScene("CardInspectorUI", LoadSceneMode.Additive);
    }

    private void OnDestroy() 
    {
        DeckCreationManager.SwitchToDeckScreen -= HandleDoneWithDeck;
        DeckSlot.OnDeckSelected -= HandleDeckSelection;
    }

    public void HandleDeckSelection(int slot) 
    {
        decksUI.SetActive(false);

        deckManager.SetUp(deckSlots[slot].deck, slot);
        cardsUI.SetActive(true);
    }

    public void HandleDoneWithDeck() 
    {
        cardsUI.SetActive(false);
        Refresh();
        decksUI.SetActive(true);
    }

    private void Refresh() 
    {
        List<DeckData> deckDatas = DeckSaveSystem.LoadAllDecks();

        int newDeckIndex = 0;
        for (int i = 0; i < deckSlots.Count; i++)
        {
            if(deckDatas.Count > i)
            {
                deckSlots[i].SetDeck(new DeckObject(deckDatas[i]));
                deckSlots[i].gameObject.SetActive(true);
            } 
            else
            { 
                newDeckIndex = i;
                break;
            }
        }

        if(deckDatas.Count != 8)
        {
            deckSlots[newDeckIndex].SetDeck(new DeckObject("New Deck"));
            deckSlots[newDeckIndex].gameObject.SetActive(true);

            for (int i = newDeckIndex + 1; i < deckSlots.Count; i++)
            {
                deckSlots[i].gameObject.SetActive(false);
            }
        }   
    }

    public void BackToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}

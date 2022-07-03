using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMulligan : GameAction
{
    public GameObject cardPrefab;
    public CardContainer cardContainer;
    private PlayerDraw playerDraw;

    private bool doneSelecting = false;
    public int startingDraw = 3;

    private List<Card> cardsToMulligan = new List<Card>();
    private List<CardController> controllers = new List<CardController>();
    private List<int> cardsToGetRidOf = new List<int>();

    public override IEnumerator ActionCoroutine()
    {
        playerDraw = Player.Instance.GetComponent<PlayerDraw>();
        ActionSequencer sequence = new ActionSequencer();
        CardController.OnSelected += HandleCardSelected;
        MultiUseButton.Instance.SetButtonFunction(DoneSelecting);
        MultiUseButton.Instance.SetButtonText("Done");
        MultiUseButton.Instance.SetInteractable(true);

        for (int i = 0; i < startingDraw; i++)
        {
            CardController card = Instantiate(cardPrefab).GetComponent<CardController>();
            // card.SetUpCardInfo(Player.Instance.GetDeck().cards[i].name);
            card.currentState = CardController.CardState.Default;   
            cardContainer.AddCard(card);
            cardsToMulligan.Add(Player.Instance.GetDeck().cards[i]);
            controllers.Add(card);
            card.potentialTarget.SetSelectable(true);
        }

        while(!doneSelecting) { yield return null; }

        for(int i = 0; i < cardsToMulligan.Count; i++)
        {
            // If the card is not one we want to get rid of, draw it
            if (!cardsToGetRidOf.Contains(i))
            {
                playerDraw.SetSpecificCard(cardsToMulligan[i]);
                sequence.AddGameAction(playerDraw);
                yield return StartCoroutine(sequence.RunSequence());
            }
        }

        Player.Instance.GetDeck().Shuffle(10);

        for(int i = 0; i < cardsToGetRidOf.Count; i++)
        {
            sequence.AddGameAction(playerDraw);
            yield return StartCoroutine(sequence.RunSequence());
        }

        foreach (CardController card in controllers)
        {
            Destroy(card.gameObject);
        }

        MultiUseButton.Instance.BackToDefault();
        MultiUseButton.Instance.SetInteractable(Player.Instance.myTurn);
        CardController.OnSelected -= HandleCardSelected;
        cardsToMulligan.Clear();
        cardsToGetRidOf.Clear();

        OnEnd();

        gameObject.SetActive(false);
    }

    public void HandleCardSelected(CardController card)
    {
        int cardIndex = controllers.IndexOf(card);

        if (cardsToGetRidOf.Contains(cardIndex))
        {
            // Debug.Log("Removing a get rid of card");
            cardsToGetRidOf.Remove(cardIndex);
            card.potentialTarget.SetSelected(false);
        }
        else
        {
            cardsToGetRidOf.Add(cardIndex);
            card.potentialTarget.SetSelected(true);
        }
    }

    public void DoneSelecting()
    {
        doneSelecting = true;
    }
}

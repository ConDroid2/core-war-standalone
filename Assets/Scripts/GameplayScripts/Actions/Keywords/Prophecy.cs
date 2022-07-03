using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prophecy : InHandKeyword
{
    CardController card;
    private void Awake()
    {
        card = GetComponent<CardController>();

        if (card.photonView.IsMine)
        {
            Player.Instance.pay.OnActionStart += HandlePayStart;
        }     
    }

    private void OnDestroy()
    {
        if (card.photonView.IsMine)
        {
            Player.Instance.pay.OnActionStart -= HandlePayStart;
        }      
    }

    public void HandlePayStart()
    {
        if(Player.Instance.pay.card == card)
        {
            MultiUseButton.Instance.SetButtonFunction(Prophecize);
            MultiUseButton.Instance.SetButtonText("Prophecize");
            MultiUseButton.Instance.SetInteractable(true);
        }     
    }

    public void Prophecize()
    {
        int prophecyLength = Player.Instance.pay.card.cardData.GetTotalCost() - MagickManager.Instance.GetTotalSelected();
        Player.Instance.pay.PayEnergy();
        MainSequenceManager.Instance.InterruptCurrentAction();
        card.currentState = CardController.CardState.Default;

        Prophecized prophecized = gameObject.AddComponent<Prophecized>();
        prophecized.SetCountdown(prophecyLength);
    }
}

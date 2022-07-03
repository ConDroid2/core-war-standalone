using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerPayForCard : GameAction
{
    [SerializeField] private Player player;
    public CardController card = null;
    private Transform waitPosition;

    public GameObject EnergyCanvas;

    private void Awake() 
    {
        player = GetComponent<Player>();
        waitPosition = GameObject.Find("CardWaitZone").transform;

        MouseManager.Instance.OnRightClick += Interrupt;
    }

    public override void PerformGameAction()
    {
        MultiUseButton.Instance.SetInteractable(false);
        MultiUseButton.Instance.SetButtonText("Waiting for payment");
        interrupted = false;
        base.PerformGameAction();
    }

    public override IEnumerator ActionCoroutine()
    {
        Tween moveToWaitPos = card.transform.DOMove(waitPosition.position, 0.5f).SetEase(Ease.InCubic);
        card.transform.DOScale(new Vector3(2, 2, 1), 0.5f);

        yield return moveToWaitPos.WaitForCompletion();

        player.ChangeMode(Player.Mode.SelectEnergy);
        foreach (string color in MagickManager.Instance.magickColors)
        {
            MagickManager.Instance.ChangeSelected(color, card.cardData.cost[color]);
        }
        MagickManager.Instance.ChangeMode(MagickManager.Mode.Spend);
        EnergyCanvas.SetActive(true);

        while (MagickManager.Instance.GetTotalSelected() < card.cardData.GetTotalCost() && !interrupted)
        {
            yield return null;
        }

        if(!interrupted)
        {
            PayEnergy();
            card.Play();
        }
        else
        {
            card.ReturnToHand();
        }

        ResetAction();
        
        OnEnd();
    }

    public void PayEnergy()
    {
        MagickManager.Instance.SpendSelected();
    }

    public void ResetAction()
    {
        MagickManager.Instance.ResetSelected();
        MagickManager.Instance.ChangeMode(MagickManager.Mode.Default);
        player.ChangeMode(Player.Mode.Normal);
        player.HandleCardUnselected();
        EnergyCanvas.SetActive(false);
        card = null;
        MultiUseButton.Instance.BackToDefault();
        MultiUseButton.Instance.SetInteractable(true);
        interrupted = false;
    }
}

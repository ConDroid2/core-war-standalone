using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SequenceSystem 
{
    public class PlayerPayForCard : GameAction
    {
        Player player;
        public CardController card;
        Transform waitPosition;
        GameObject energyCanvas;
        public PlayerPayForCard(Player player, GameObject energyCanvas, Transform waitPosition)
        {
            this.player = player;
            this.energyCanvas = energyCanvas;
            this.waitPosition = waitPosition;
        }

        public PlayerPayForCard(PlayerPayForCard template) : base(template)
        {
            player = template.player;
            energyCanvas = template.energyCanvas;
            waitPosition = template.waitPosition;
            card = template.card;
        }

        public override void StartGameAction()
        {
            MultiUseButton.Instance.SetInteractable(false);
            MultiUseButton.Instance.SetButtonText("Waiting for payment");
            interrupted = false;
            base.StartGameAction();
        }

        public override void PerformGameAction()
        {
            card.transform.DOMove(waitPosition.position, 0.5f).SetEase(Ease.InCubic);
            card.transform.DOScale(new Vector3(2, 2, 1), 0.5f).OnComplete(() =>
            {
                MagickManager.Instance.OnSelectedChange += HandleSelectedChange;
                MouseManager.Instance.OnRightClick += Interrupt;
                foreach (string color in MagickManager.Instance.magickColors)
                {
                    // Debug.Log("Selecting energy for " + color);
                    MagickManager.Instance.ChangeSelected(color, card.cardData.cost[color]);
                }

                if (card.cardData.cost["Neutral"] > 0)
                {
                    MagickManager.Instance.ChangeMode(MagickManager.Mode.Spend);
                    energyCanvas.SetActive(true);
                }
            });
        }

        public void HandleSelectedChange(string color, int amount)
        {
            if(MagickManager.Instance.GetTotalSelected() == card.cardData.GetTotalCost())
            {
                MagickManager.Instance.OnSelectedChange -= HandleSelectedChange;
                MouseManager.Instance.OnRightClick -= Interrupt;
                PayEnergy();
                card.Play();

                ResetAction();
                OnEnd();
            }
        }

        public override void Interrupt()
        {
            MagickManager.Instance.OnSelectedChange -= HandleSelectedChange;
            MouseManager.Instance.OnRightClick -= Interrupt;
            card.ReturnToHand();
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
            energyCanvas.SetActive(false);
            card = null;
            MultiUseButton.Instance.BackToDefault();
            MultiUseButton.Instance.SetInteractable(true);
            interrupted = false;
        }
    }
}

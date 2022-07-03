using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SequenceSystem 
{
    public class InHandSpellPlay : GameAction, NetworkedAction
    {
        CardController controller;
        Transform waitPos;
        public InHandSpellPlay(CardController controller, Transform waitPos)
        {
            this.controller = controller;
            this.waitPos = waitPos;
            gameObject = controller.gameObject;
        }

        public InHandSpellPlay(InHandSpellPlay template)
        {
            controller = template.controller;
            waitPos = template.waitPos;
            gameObject = template.gameObject;
        }

        public override void PerformGameAction()
        {
            if (controller.transform.position != waitPos.position)
            {
                controller.transform.DOMove(waitPos.position, 0.5f).SetEase(Ease.InCubic);
                controller.transform.DOScale(new Vector3(2, 2, 1), 0.5f).OnComplete(CardInPlace);

            }
            else
            {
                CardInPlace();
            }
        }

        public void CardInPlace()
        {
            controller.col.enabled = true;
            controller.currentState = CardController.CardState.Waiting;
            controller.Reveal();

            if (!controller.photonView.IsMine)
            {
                // doneLooking = false;
                MultiUseButton.Instance.SetButtonFunction(DoneLooking);
                MultiUseButton.Instance.SetButtonText("OK");
                MultiUseButton.Instance.SetInteractable(true);
                MultiUseButton.Instance.buttonTimer.StartTimer(10f);
            }
            else
            {
                MultiUseButton.Instance.SetInteractable(false);
                MultiUseButton.Instance.SetButtonText("Waiting . . .");
                MultiUseButton.Instance.buttonTimer.Pause();
                NetworkEventReceiver.OnNetworkEvent += HandleOpponentDoneLooking;
            }
        }

        public void HandleOpponentDoneLooking()
        {
            MultiUseButton.Instance.BackToDefault();
            MultiUseButton.Instance.SetInteractable(Player.Instance.myTurn);
            NetworkEventReceiver.OnNetworkEvent -= HandleOpponentDoneLooking;

            if (controller.photonView.IsMine)
            {
                if (!interrupted)
                {
                    controller.InvokeOnPlay();

                    Player.Instance.spellManager.AddSpellToList(controller);
                    TriggerManager.Instance.InvokeOnCardPlayed(controller.cardData);
                }
                else
                {
                    if (controller.cardData.script != "")
                    {
                        controller.GetComponent<CardScript>().InHandDeath();
                    }
                    // GameObject.Destroy(controller.gameObject);
                }

                MultiUseButton.Instance.buttonTimer.UnPause();
                Player.Instance.hand.RemoveCard(controller);
                controller.Remove();
            }
            else
            {
                Enemy.Instance.hand.RemoveCard(controller);
            }

            OnEnd();
        }

        public void DoneLooking()
        {
            object[] eventData = { };
            NetworkEventSender.Instance.SendEvent(eventData, NetworkingUtilities.eventDictionary["DoneWaitingForOpponent"]);
            MultiUseButton.Instance.buttonTimer.EndTimer();

            HandleOpponentDoneLooking();
        }
    }
}

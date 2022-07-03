using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class InHandSpellPlay : GameAction, NetworkedAction
{
    private CardController card;
    private Transform waitPos;
    private bool doneLooking = false;

    public List<Ability> abilities = new List<Ability>();

    private void Awake() 
    {
        card = GetComponent<CardController>();
        // card.play = this;
        waitPos = GameObject.Find("CardWaitZone").transform;
    }


    public override IEnumerator ActionCoroutine()
    {
        if(transform.position != waitPos.position)
        {
            Tween moveToWaitPos = transform.DOMove(waitPos.position, 0.5f).SetEase(Ease.InCubic);
            transform.DOScale(new Vector3(2, 2, 1), 0.5f);

            yield return moveToWaitPos.WaitForCompletion();
            
        }

        card.col.enabled = true;
        card.currentState = CardController.CardState.Waiting;
        card.Reveal();

        if(!card.photonView.IsMine)
        {
            doneLooking = false;
            MultiUseButton.Instance.SetButtonFunction(DoneLooking);
            MultiUseButton.Instance.SetButtonText("OK");
            MultiUseButton.Instance.SetInteractable(true);      
        }
        else
        {
            MultiUseButton.Instance.SetInteractable(false);
            MultiUseButton.Instance.SetButtonText("Waiting . . .");
        }

        // Wait for the opponent to finish observing the card
        while (!doneLooking)
        {
            yield return null;
        }

        if (card.photonView.IsMine)
        {
            if (!interrupted)
            {
                card.InvokeOnPlay();

                ActionSequencer sequence = new ActionSequencer();
                sequence.AddGameAction(abilities);
                yield return StartCoroutine(sequence.RunSequence());

                Player.Instance.spellManager.AddSpellToList(card);
            }
            else
            {
                if (card.cardData.script != "")
                {
                    card.GetComponent<CardScript>().OnDeath();
                }
                Destroy(card.gameObject);
            }

            Player.Instance.hand.RemoveCard(card);
        }

        card.Remove();

        OnEnd();
    }

    public void DoneLooking()
    {
        object[] rpcData = { };
        card.photonView.RPC("DoneLookingRPC", RpcTarget.All, rpcData);

    }

    [PunRPC]
    public void DoneLookingRPC()
    {
        doneLooking = true;
        MultiUseButton.Instance.BackToDefault();
        MultiUseButton.Instance.SetInteractable(Player.Instance.myTurn);
    }
}

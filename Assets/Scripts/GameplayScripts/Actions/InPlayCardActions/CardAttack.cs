using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class CardAttack : TargetedAction, NetworkedAction
{
    private void Awake() 
    {
        targetType = typeof(UnitController);
    }

    public override IEnumerator ActionCoroutine()
    {
        
        UnitController card = GetComponent<UnitController>();
        card.interactable = false;
        Vector3 originalPosition = transform.position;

        Tween moveToAttack = transform.DOMove(target.transform.position + new Vector3(0, -1f, -0.3f), 0.15f);
        yield return moveToAttack.WaitForCompletion();

        if (card.photonView.IsMine)
        {
            UnitController enemy = target.GetComponent<UnitController>();
            // Store the attack values since some cards can change their attack stats mid-attack, which shouldn't effec this
            int attackerDamage = card.cardData.currentStrength;
            int defenderDamage = enemy.cardData.currentStrength;
            // Attacker deal damage to target
            object[] rpcData = { defenderDamage };
            card.photonView.RPC("RPCTakeDamage", RpcTarget.All, rpcData);
            // Target deal damage to attacker
            object[] enemyRPC = { attackerDamage };
            enemy.photonView.RPC("RPCTakeDamage", RpcTarget.All, enemyRPC);

            card.OnDealDamageTo?.Invoke(enemy);
            enemy.OnDealDamageTo?.Invoke(card);

            card.SetAttackActionState(UnitController.ActionState.Acted);
            card.SetMoveActionState(UnitController.ActionState.Acted);
        }

        yield return new WaitForSeconds(0.1f);

        Tween moveBackToOriginalPosition = transform.DOMove(originalPosition, 0.2f);
        yield return moveBackToOriginalPosition.WaitForCompletion();

        OnEnd();
        card.interactable = true;
    }

    [PunRPC]
    public void SetAttackTarget(int photonID)
    {
        target = PhotonView.Find(photonID).gameObject.GetComponent(targetType);
    }
}

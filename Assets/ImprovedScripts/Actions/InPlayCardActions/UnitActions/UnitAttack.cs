using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using DG.Tweening;

namespace SequenceSystem 
{
    public class UnitAttack : GameAction
    {
        protected UnitController controller;
        protected UnitController target;
        public UnitAttack(UnitController controller)
        {
            this.controller = controller;
            gameObject = controller.gameObject;
        }

        public UnitAttack(UnitAttack template)
        {
            controller = template.controller;
            gameObject = controller.gameObject;
        }

        public override void PerformGameAction()
        {
            controller.interactable = false;
            Vector3 originalPosition = controller.transform.position;

            controller.transform.DOMove(target.transform.position + new Vector3(0, -1f, -0.3f), 0.15f).OnComplete(() =>
            {

                if (controller.isMine)
                {
                    // Store the attack values since some cards can change their attack stats mid-attack, which shouldn't effect this
                    int attackerDamage = controller.cardData.currentStrength;
                    int defenderDamage = target.cardData.currentStrength;
                    // Attacker deal damage to target
                    target.takeDamage(attackerDamage);
                    // Target deal damage to attacker
                    controller.takeDamage(defenderDamage);

                    controller.OnDealDamageTo?.Invoke(target);
                    target.OnDealDamageTo?.Invoke(controller);

                    controller.SetAttackActionState(UnitController.ActionState.Acted);
                    controller.SetMoveActionState(UnitController.ActionState.Acted);
                }

                var sequence = DOTween.Sequence();
                sequence.AppendInterval(0.1f);

                Tween backToPosition = controller.transform.DOMove(originalPosition, 0.2f).OnComplete(() =>
                {
                    OnEnd();
                    controller.interactable = true;
                });

                sequence.Append(backToPosition);

                sequence.Play();
            });
        }

        public void SetAttackTarget(UnitController newTarget)
        {
            target = newTarget;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class Swashbuckler : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        SwashbucklerAttack attack = new SwashbucklerAttack(unit);
        unit.unitAttack = attack;
        unit.AttackStack.Clear();
        unit.AttackStack.Add(attack);
        unit.GetComponent<GameActionList>().actions.Add(attack);
    }

    public class SwashbucklerAttack : SequenceSystem.UnitAttack
    {
        public SwashbucklerAttack(UnitController unit) : base(unit)
        {

        }
        public SwashbucklerAttack(SwashbucklerAttack template) : base(template) { }

        public override void PerformGameAction()
        {
            controller.interactable = false;
            Vector3 originalPosition = controller.transform.position;
            Debug.Log("Doing the swashbuckler attack");
            controller.transform.DOMove(target.transform.position + new Vector3(0, -1f, -0.3f), 0.15f).OnComplete(() =>
            {

                if (controller.isMine)
                {
                    // Store the attack values since some cards can change their attack stats mid-attack, which shouldn't effect this
                    int attackerDamage = controller.cardData.currentStrength;
                    int defenderDamage = target.cardData.currentStrength;
                    // Deal damage to target
                    target.takeDamage(attackerDamage);

                    controller.OnDealDamageTo?.Invoke(target);

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



    }
}

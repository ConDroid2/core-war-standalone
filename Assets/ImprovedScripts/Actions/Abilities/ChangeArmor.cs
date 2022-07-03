using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem
{
    public class ChangeArmor : TargetedAbility
    {
        private int changeAmount;
        CardParent controller;

        public ChangeArmor(int changeAmount, CardParent controller)
        {
            this.changeAmount = changeAmount;
            this.controller = controller;
        }

        public ChangeArmor(ChangeArmor template)
        {
            changeAmount = template.changeAmount;
            controller = template.controller;
            target = template.target;
        }

        public override void PerformGameAction()
        {
            if(target == null)
            {
                target = controller.gameObject;
            }
            UnitController targetUnit = target.GetComponent<UnitController>();
            targetUnit.ChangeArmor(changeAmount);

            OnEnd();
        }
    }
}

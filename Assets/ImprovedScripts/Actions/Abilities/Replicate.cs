using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class Replicate : TargetedAbility
    {
        CardParent controller;
        public Replicate(CardParent controller)
        {
            this.controller = controller;
        }

        public Replicate(Replicate template) : base(template)
        {
            controller = template.controller;
        }

        public override void PerformGameAction()
        {
            Summon summon = new Summon(1, target.GetComponent<UnitController>().cardData.name, controller.gameObject);
            MainSequenceManager.Instance.Add(summon);
            OnEnd();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class TargetedTransformation : TargetedAbility
    {
        InPlayCardController controller;
        public TargetedTransformation(InPlayCardController controller)
        {
            this.controller = controller;
        }

        public TargetedTransformation(TargetedTransformation template)
        {
            controller = template.controller;
            target = template.target;
        }

        public override void PerformGameAction()
        {
            string transformInto = target.GetComponent<InPlayCardController>().cardData.name;

            Transformation transformation = new Transformation(transformInto, controller);

            MainSequenceManager.Instance.Add(transformation);

            OnEnd();
        }
    }
}

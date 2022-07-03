using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class AddStatus : TargetedAbility
    {
        string status = "";
        int count = 0;
        public AddStatus(string status, int count)
        {
            this.status = status;
            this.count = count;
        }

        public AddStatus(AddStatus template) : base(template)
        {
            status = template.status;
            count = template.count;
        }

        public override void PerformGameAction()
        {
            UnitController unit = target.GetComponent<UnitController>();

            unit.AddStatus(status, count);

            OnEnd();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class RemoveStatuses : TargetedAbility
    {
        public RemoveStatuses()
        {
            
        }

        public RemoveStatuses(RemoveStatuses template)
        {
            target = template.target;
        }

        public override void PerformGameAction()
        {
            UnitController unit = target.GetComponent<UnitController>();

            List<string> statuses = new List<string>(unit.cardData.activeStatuses);

            foreach (string status in statuses)
            {
                unit.RemoveStatus(status);
            }

            OnEnd();
        }
    }
}

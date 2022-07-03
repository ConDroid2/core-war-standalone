using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class RemoveKeyword : TargetedAbility
    {
        string keywordName;
        public RemoveKeyword(string keywordName, UnitController unit = null)
        {
            this.keywordName = keywordName;
            if(unit != null)
            {
                SetTarget(unit.gameObject);
            }       
        }

        public RemoveKeyword(RemoveKeyword template)
        {
            keywordName = template.keywordName;
            target = template.target;
        }

        public override void PerformGameAction()
        {
            UnitController unit = target.GetComponent<UnitController>();
            unit.RemoveKeyword(keywordName);

            OnEnd();
        }
    }
}

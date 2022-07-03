using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class KnockbackUnit : TargetedAbility
    {
        public KnockbackUnit()
        {
            
        }

        public KnockbackUnit(KnockbackUnit template)
        {
            target = template.target;
        }

        public override void PerformGameAction()
        {
            Debug.Log("Knockback unit");
            if(target.GetComponent<UnitController>() != null)
            {
                MainSequenceManager.Instance.Add(target.GetComponent<UnitController>().unitMoveBack);
            }       

            OnEnd();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class ResetLevels : Ability
    {
        public ResetLevels()
        {
            
        }

        public ResetLevels(ResetLevels template)
        {
            
        }

        public override void PerformGameAction()
        {
            MagickManager manager = MagickManager.Instance;
            foreach(string color in manager.magickColors)
            {
                manager.ChangeLevel(color, manager.level[color] * -1);
            }

            OnEnd();
        }
    }
}

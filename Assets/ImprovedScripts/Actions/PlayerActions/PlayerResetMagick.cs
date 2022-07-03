using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem
{
    public class PlayerResetMagick : GameAction
    {
        public PlayerResetMagick() { }
        public PlayerResetMagick(PlayerResetMagick template) { }

        public override void PerformGameAction()
        {
            MagickManager manager = MagickManager.Instance;
            manager.FillMagick();
            manager.ResetSelected();

            OnEnd();
        }
    }
}

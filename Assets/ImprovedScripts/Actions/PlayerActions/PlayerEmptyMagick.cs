using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SequenceSystem;

namespace SequenceSystem
{
    public class PlayerEmptyMagick : GameAction
    {
        private MagickManager magickManager;

        public PlayerEmptyMagick() : base()
        {
            magickManager = MagickManager.Instance;
        }

        public PlayerEmptyMagick(PlayerEmptyMagick template)
        {
            magickManager = template.magickManager;
        }

        public override void PerformGameAction()
        {
            magickManager.EmptyMagick();
            OnEnd();
        }
    }
}

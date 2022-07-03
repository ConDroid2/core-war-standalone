using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class DrawToHandSize : Ability
    {
        public DrawToHandSize()
        {
            
        }

        public DrawToHandSize(DrawToHandSize template)
        {
            
        }

        public override void PerformGameAction()
        {
            Player player = Player.Instance;
            int amountToDraw = player.handSize - player.hand.Count;

            if(amountToDraw > 0)
            {
                for(int i = 0; i < amountToDraw; i++)
                {
                    MainSequenceManager.Instance.Add(player.draw);
                }
            }

            OnEnd();
        }
    }
}

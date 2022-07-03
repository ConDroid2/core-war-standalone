using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class PlayerDeactivateUnits : GameAction
    {
        Player player;
        public PlayerDeactivateUnits(Player player) : base()
        {
            this.player = player;
        }

        public PlayerDeactivateUnits(PlayerDeactivateUnits template)
        {
            player = template.player;
        }

        public override void PerformGameAction()
        {
            if (player)
            {
                for (int i = 0; i < player.zones.Count - 1; i++)
                {
                    Zone zone = player.zones[i];
                    // Potential issue since zones technically contain CardParents
                    foreach (CardParent parent in zone.cards.cards)
                    {
                        if (parent.cardData.type == CardUtilities.Type.Character)
                        {
                            UnitController card = parent as UnitController;
                            // Set the state manually so that things like Sabrefang aren't triggered
                            card.moveState = UnitController.ActionState.Acted;
                            card.attackState = UnitController.ActionState.Acted;
                            card.actedThisTurn = true;
                        }
                    }
                }
            }

            OnEnd();
        }
    }
}

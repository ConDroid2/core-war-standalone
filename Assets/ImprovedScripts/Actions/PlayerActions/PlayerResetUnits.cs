using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem
{
    public class PlayerResetUnits : GameAction
    {
        Player player;

        public PlayerResetUnits(Player player)
        {
            this.player = player;
        }

        public PlayerResetUnits(PlayerResetUnits template)
        {
            player = template.player;
        }

        public override void PerformGameAction()
        {
            for (int i = 0; i < player.zones.Count; i++)
            {
                Zone zone = player.zones[i];
                // Potential issue since zones technically contain CardParents
                foreach (CardParent parent in zone.cards.cards)
                {
                    if (parent.cardData.type != CardUtilities.Type.Support)
                    {
                        UnitController card = parent as UnitController;
                        card.SetAttackActionState(UnitController.ActionState.CanAct);
                        card.SetMoveActionState(UnitController.ActionState.CanAct);
                        card.actedThisTurn = false;
                    }
                }
            }

            OnEnd();
        }
    }
}

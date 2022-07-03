using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeactivateCards : GameAction
{
    [HideInInspector] public Player player = null;

    private void Awake() 
    {
        player = GetComponent<Player>();
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
                    UnitController card = parent as UnitController;
                    // Set the state manually so that things like Sabrefang aren't triggered
                    card.moveState = UnitController.ActionState.Acted;
                    card.attackState = UnitController.ActionState.Acted;
                    card.actedThisTurn = true;
                }
            }
        }

        OnEnd();
    }
}

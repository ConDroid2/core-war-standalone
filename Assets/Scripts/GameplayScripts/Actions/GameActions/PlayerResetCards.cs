using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetCards : GameAction
{
    [HideInInspector] public Player player = null;

    private void Awake() 
    {
        player = GetComponent<Player>();
    }

    public override void PerformGameAction() 
    {
        for (int i = 0; i < player.zones.Count; i++)
        {
            Zone zone = player.zones[i];
            // Potential issue since zones technically contain CardParents
            foreach (UnitController card in zone.cards.cards)
            {
                card.SetAttackActionState(UnitController.ActionState.CanAct);
                card.SetMoveActionState(UnitController.ActionState.CanAct);
                card.actedThisTurn = false;
            }
        }

        OnEnd();
    }
}

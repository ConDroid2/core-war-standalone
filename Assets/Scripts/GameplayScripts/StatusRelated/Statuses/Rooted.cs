using UnityEngine;

public class Rooted : Status
{

    protected override void Awake()
    {
        base.Awake();

        if (card.isMine)
        {
            card.OnMoveStateChanged += RootUnit;
        }
    }
    public override void PerformStatus()
    {
        // card.SetMoveActionState(UnitController.ActionState.Acted);
        card.moveState = UnitController.ActionState.Acted;
    }

    public void RootUnit(UnitController.ActionState state)
    {
        if(state == UnitController.ActionState.CanAct)
        {
            Debug.Log("Rooting unit");
            PerformStatus();
        }  
    }

    public override void RemoveStatus()
    {
        base.RemoveStatus();

        if (card.isMine)
        {
            card.OnMoveStateChanged -= RootUnit;

            if (!card.actedThisTurn)
            {
                card.SetMoveActionState(UnitController.ActionState.CanAct);
            }
        }
    }
}

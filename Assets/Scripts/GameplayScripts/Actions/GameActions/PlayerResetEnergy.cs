using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetEnergy : GameAction
{
    private MagickManager magickManager;

    private void Awake() 
    {
        magickManager = MagickManager.Instance;
    }

    public override IEnumerator ActionCoroutine()
    {
        magickManager.FillMagick();
        magickManager.ResetSelected();
        OnEnd();

        return null;
    }
}

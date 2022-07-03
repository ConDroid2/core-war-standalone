using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmptyEnergy : GameAction
{
    [HideInInspector] public MagickManager magickManager;
    // Start is called before the first frame update
    private void Awake()
    {
        magickManager = MagickManager.Instance;
    }

    public override IEnumerator ActionCoroutine()
    {
        if(!magickManager)
        {
            magickManager = MagickManager.Instance;
        }
        magickManager.EmptyMagick();
        OnEnd();
        return null;
    }
}

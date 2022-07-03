using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NashatAgelessOne : CardScript
{
    public override void InPlaySetUp()
    {
        GetComponent<Legendary>().Initialize("Wither");
        Player.Instance.draw.overdrawAction = new SequenceSystem.Win(true);
    }

    public override void InPlayDeath()
    {
        Player.Instance.draw.overdrawAction = new SequenceSystem.Lose(true);
    }
}

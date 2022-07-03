using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayableAreaController : MonoBehaviour
{
    public UnityEvent OnDisabled;

    public bool ZoneArea = false;
    public int zoneNum = 0;

    private void OnDisable()
    {
        OnDisabled?.Invoke();
    }
    public void FlagCurrentCardAsPlayable()
    {
        if(Player.Instance.currentCard != null)
        {
            Player.Instance.currentCard.SetCanBePlayed(true);

            if (ZoneArea)
            {
                (Player.Instance.currentCard.play as SequenceSystem.InHandSupportPlay).SetZoneToPlayTo(zoneNum);
            }
        }
    }

    public void FlagCurrentCardAsNotPlayable()
    {
        if (Player.Instance.currentCard != null)
        {
            Player.Instance.currentCard.SetCanBePlayed(false);
        }
    }
}

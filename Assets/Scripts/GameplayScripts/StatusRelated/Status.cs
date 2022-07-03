using UnityEngine;
using System;

public class Status : MonoBehaviour
{
    public int turnCount;

    protected UnitController card;
    
    /** VFX variables **/
    protected string vfxPath = "DefaultStatus";
    private GameObject vfxObject;

    public Action<int> OnAmountChanged;

    protected virtual void Awake()
    {
        card = GetComponent<UnitController>();

        if (card.photonView.IsMine)
        {
            Player.Instance.OnEndTurn += DecreaseCount;
        }
        else
        {
            Player.Instance.OnStartTurn += DecreaseCount;
        }

        AddVFX();
        
    }

    public void AddVFX()
    {
        vfxObject = Instantiate(Resources.Load<GameObject>("VFX/Statuses/" + vfxPath), card.transform);
        vfxObject.GetComponent<StatusVFXController>().SetStatusController(this);
    }

    public void SetStatusCount(int newCount)
    {
        turnCount = newCount;

        OnAmountChanged?.Invoke(turnCount);
    }


    public void DecreaseCount()
    {
        turnCount--;

        OnAmountChanged?.Invoke(turnCount);

        if (turnCount == 0)
        {
            RemoveStatus();
        }
    }

    public virtual void PerformStatus() { Debug.Log("Go status"); }

    public virtual void RemoveStatus() 
    { 
        if (card.photonView.IsMine)
        {
            Player.Instance.OnEndTurn -= DecreaseCount;
        }
        else
        {
            Player.Instance.OnStartTurn -= DecreaseCount;
        }
        if (card.cardData.activeStatuses.Contains(GetType().ToString()))
        {
            card.cardData.activeStatuses.Remove(GetType().ToString());
        }
        Destroy(vfxObject);
        Destroy(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectableEnergyGraphicsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amount;
    [SerializeField] private GameObject hover;

    public string color = "";

    private void Awake()
    {
        MagickManager.Instance.OnModeChange += HandleModeChange;
        MagickManager.Instance.OnCurrentChange += HandleCurrentChanged;
        MagickManager.Instance.OnLevelChange += HandleLevelChanged;
        MagickManager.Instance.OnSelectedChange += HandleSelectedChanged;
    }

    public void HandleModeChange(MagickManager.Mode newMode)
    {
        hover.SetActive(false);
        switch (newMode)
        {
            case MagickManager.Mode.IncreaseCurrent:
                SetAmount(MagickManager.Instance.current[color]);
                break;
            case MagickManager.Mode.IncreaseLevel:
                SetAmount(MagickManager.Instance.level[color]);
                break;
            case MagickManager.Mode.Spend:
                SetAmount(MagickManager.Instance.current[color] - MagickManager.Instance.selected[color]);
                break;
            case MagickManager.Mode.Default:
                break;
            default:
                Debug.Log("Something went wrong in the energy graphics controller");
                break;
        }
    }

    public void HandleCurrentChanged(string color, int amount)
    {
        if (color != this.color) return;
        if(MagickManager.Instance.currentMode == MagickManager.Mode.Spend)
        {
            SetAmount(amount - 1);
        }
        else if(MagickManager.Instance.currentMode == MagickManager.Mode.IncreaseCurrent)
        {
            SetAmount(amount + 1);
        }
    }

    public void HandleLevelChanged(string color, int amount)
    {
        if (color != this.color) return;
        if(MagickManager.Instance.currentMode == MagickManager.Mode.IncreaseLevel)
        {
            SetAmount(amount + 1);
        }
    }

    public void HandleSelectedChanged(string color, int amount)
    {
        if (color != this.color) return;
        if(MagickManager.Instance.currentMode == MagickManager.Mode.Spend)
        {
            SetAmount(MagickManager.Instance.current[color] - amount - 1);
        }
    }

    public void HandleHoverEnter()
    {
        MagickManager.Mode mode = MagickManager.Instance.currentMode;
        if(mode == MagickManager.Mode.IncreaseCurrent)
        {
            SetAmount(MagickManager.Instance.current[color] + 1);
        }
        else if(mode == MagickManager.Mode.IncreaseLevel)
        {
            if(MagickManager.Instance.level[color] != EnergyUtilities.maxLevel)
                SetAmount(MagickManager.Instance.level[color] + 1);
        }
        else if(mode == MagickManager.Mode.Spend)
        {
            if(MagickManager.Instance.current[color] - MagickManager.Instance.selected[color] > 0)
            {
                SetAmount(MagickManager.Instance.current[color] - MagickManager.Instance.selected[color] - 1);
            }          
        }
    }

    public void HandleHoverExit()
    {
        // The mode is not changing, but the HandleModeChange function does what we want it to
        HandleModeChange(MagickManager.Instance.currentMode);
    }

    public void SetAmount(int newAmount)
    {
        amount.text = newAmount.ToString();
    }
}

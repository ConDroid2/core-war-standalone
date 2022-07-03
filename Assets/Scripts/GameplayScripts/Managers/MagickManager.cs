using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagickManager : MonoBehaviour
{
    [SerializeField] private GameObject energyUI;
    public string[] magickColors = { "Blue", "Red", "Green", "Black" };
    public string[] costColors = { "Blue", "Red", "Green", "Black", "Neutral" };

    // How much you have right now
    public Dictionary<string, int> current = new Dictionary<string, int>
    {
        { "Blue", 0 },
        { "Red", 0 },
        { "Green", 0 },
        { "Black", 0 }
    };

    // What your total is
    public Dictionary<string, int> level = new Dictionary<string, int>
    {
        { "Blue", 0 },
        { "Red", 0 },
        { "Green", 0 },
        { "Black", 0 }
    };

    // How much will be spent
    public Dictionary<string, int> selected = new Dictionary<string, int>
    {
        { "Blue", 0 },
        { "Red", 0 },
        { "Green", 0 },
        { "Black", 0 }
    };

    public enum Mode { Default, IncreaseLevel, IncreaseCurrent, Spend };
    public Mode currentMode = Mode.Default;

    public event Action<Mode> OnModeChange;
    public event Action<string, int> OnCurrentChange;
    public event Action<string, int> OnLevelChange;
    public event Action<string, int> OnSelectedChange;

    public static MagickManager Instance;

    public void SetUp()
    {
        if (Instance == null) Instance = this;
        SelecetableEnergy.OnEnergySelected += HandleMagickSelected;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            string message = "Selected Energy: \n";
            foreach(string color in magickColors)
            {
                message += color + ": " + selected[color] + '\n';
            }
            Debug.Log(message);
        }
    }

    public void HandleMagickSelected(string color)
    {
        if(currentMode == Mode.IncreaseLevel)
        {
            ChangeLevel(color, 1);
        }
        else if(currentMode == Mode.IncreaseCurrent)
        {
            ChangeCurrent(color, 1);
        }
        else if(currentMode == Mode.Spend)
        {
            ChangeSelected(color, 1);      
        }
    }

    public void FillMagick()
    {
        foreach(string magick in magickColors)
        {
            ChangeCurrent(magick, level[magick] - current[magick]);
        }
    }

    public void EmptyMagick()
    {
        foreach(string magick in magickColors)
        {
            ChangeCurrent(magick, -1 * current[magick]);
        }
    }

    public void SpendSelected()
    {
        foreach(string magick in magickColors)
        {
            ChangeCurrent(magick, -1 * selected[magick]);
        }

        ResetSelected();
    }

    public void ResetSelected()
    {
        foreach(string magick in magickColors)
        {
            ChangeSelected(magick, -1 * selected[magick]);
        }
    }

    public int GetTotalSelected()
    {
        return (selected["Blue"] + selected["Red"] + selected["Green"] + selected["Black"]);
    }

    /** Directly Changing Value Functions **/ 
    public void ChangeMode(Mode newMode)
    {
        currentMode = newMode;
        OnModeChange?.Invoke(currentMode);

        // Set energy UI active if mode is not default
        energyUI.SetActive(currentMode != Mode.Default);
    }

    public void ChangeCurrent(string color, int amount)
    {
        current[color] += amount;
        if (current[color] < 0) current[color] = 0;
        Player.Instance.CheckAllCardsCanBeyPlayed();
        SendEnergyChange(color);
    }

    public void ChangeLevel(string color, int amount)
    {
        level[color] = Mathf.Clamp(level[color] + amount, 0, EnergyUtilities.maxLevel);
        SendEnergyChange(color);
    }

    public void ChangeSelected(string color, int amount)
    {
        if(current[color] - selected[color] - amount >= 0)
        {
            selected[color] += amount;
            OnSelectedChange?.Invoke(color, selected[color]);
        }
    }

    // Fire events for energy change
    public void SendEnergyChange(string color)
    {
        OnLevelChange?.Invoke(color, level[color]);
        OnCurrentChange?.Invoke(color, current[color]);

        object[] eventContent = { color, level[color], current[color] };
        NetworkEventSender.Instance.SendEvent(eventContent, NetworkingUtilities.eventDictionary["EnemyEnergyChange"]);
    }

    public int GetTotalLevel()
    {
        int total = 0;
        foreach(string color in magickColors)
        {
            total += level[color];
        }

        return total;
    }
}

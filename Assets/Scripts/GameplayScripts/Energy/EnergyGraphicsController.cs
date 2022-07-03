using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyGraphicsController : MonoBehaviour
{
    [SerializeField] string color;
    [SerializeField] private TextMeshProUGUI energyText;
    private int currentCrystals = 0;
    private int totalEnergy = 0;
    private int currentEnergy = 0;

    public void SetUp() 
    {
        MagickManager.Instance.OnCurrentChange += HandleCurrentEnergyChange;
        MagickManager.Instance.OnLevelChange += HandleEnergyLevelChange;
    }

    private void OnDestroy() 
    {
        MagickManager.Instance.OnCurrentChange += HandleCurrentEnergyChange;
        MagickManager.Instance.OnLevelChange += HandleEnergyLevelChange;
    }

    public void HandleCurrentEnergyChange(string color, int amount) 
    {
        if (color == this.color)
        {
            if (amount < 0) amount = 0;
            currentEnergy = amount;
            UpdateEnergyText();
        }  
    }

    public void HandleEnergyLevelChange(string color, int amount)
    {
        if (color == this.color)
        {
            amount = Mathf.Clamp(amount, 0, EnergyUtilities.maxLevel);
            totalEnergy = amount;
            UpdateEnergyText();
        }
    }

    private void UpdateEnergyText()
    {
        energyText.text = currentEnergy.ToString() + "/" + totalEnergy.ToString();
    }
}

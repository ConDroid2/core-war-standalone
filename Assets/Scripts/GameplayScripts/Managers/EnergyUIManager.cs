using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyUIManager : MonoBehaviour
{

    //UI References
    [SerializeField] private TextMeshProUGUI energyAmount;
    [SerializeField] private TextMeshProUGUI savedEnergyAmount;

    [SerializeField] private GameObject savedEnergyBackground;

    [SerializeField] private List<EnergyGraphicsController> energyControllers = new List<EnergyGraphicsController>();

    public void SetUp() 
    {
        foreach(EnergyGraphicsController controller in energyControllers)
        {
            controller.SetUp();
        }
    }

    // Change the text based on the new amount
    private void HandleEnergyAmountChange(int amount) 
    {
        energyAmount.text = amount.ToString();
    }

    // Change the text based on the new amoutn
    private void HandleSavedEnergyAmountChange(int amount) 
    {
        // If greater than 0, set active
        if (amount > 0)
        {
            savedEnergyBackground.SetActive(true);
            savedEnergyAmount.text = amount.ToString();
        } 
        // If less than or equal to zero, deactive
        else
        {
            savedEnergyBackground.SetActive(false);
        }
    }

}

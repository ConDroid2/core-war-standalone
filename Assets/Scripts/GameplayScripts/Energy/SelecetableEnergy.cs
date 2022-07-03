using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelecetableEnergy : MonoBehaviour
{
    [SerializeField] private string color;

    /** Events **/
    public static event Action<string> OnEnergySelected;
    public static event Action<string> OnEnergyDeselected;

    public void OnClick(PointerEventData eventData) 
    {
        OnEnergySelected?.Invoke(color);
    }
}

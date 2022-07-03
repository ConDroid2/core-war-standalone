using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusVFXController : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;

    public Status statusController;

    public void SetStatusController(Status controller)
    {
        statusController = controller;
        statusController.OnAmountChanged += HandleAmountChanged;
    }

    public void HandleAmountChanged(int newAmount)
    {
        text.text = newAmount.ToString();
    }
}

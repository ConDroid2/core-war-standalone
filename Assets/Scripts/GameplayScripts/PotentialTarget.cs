using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentialTarget : MonoBehaviour
{
    [SerializeField] private GameObject selectableHover;
    [SerializeField] private GameObject selectedHover;
    public bool isSelectable = false;

    public void SetSelectable(bool selectable)
    {
        isSelectable = selectable;
        selectableHover.SetActive(selectable);
        selectedHover.SetActive(false);
    }

    public void SetSelected(bool selected)
    {
        selectableHover.SetActive(!selected);
        selectedHover.SetActive(selected);
    }
}

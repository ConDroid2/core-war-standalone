using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProphecizedVFX : MonoBehaviour
{
    [SerializeField] private TextMeshPro countText;

    public Prophecized prophecized;

    public void SetProphecized(Prophecized prophecized)
    {
        this.prophecized = prophecized;
        prophecized.OnCountChanged += HandleCountChanged;
    }

    public void HandleCountChanged(int newCount)
    {
        countText.text = newCount.ToString();
    }
}

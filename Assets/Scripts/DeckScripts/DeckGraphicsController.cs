using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckGraphicsController : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;

    public void SetAmount(int amount)
    {
        textMesh.text = amount.ToString();
    }
}

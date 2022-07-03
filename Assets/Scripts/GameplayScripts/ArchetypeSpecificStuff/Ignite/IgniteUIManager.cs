using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IgniteUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro igniteCount;

    IgniteManager manager;
    private void Awake()
    {
        manager = GetComponent<IgniteManager>();
        manager.IgniteCountChange += HandleIgniteCountChange;
    }

    public void HandleIgniteCountChange(int count)
    {
        igniteCount.text = count.ToString();
    }
}

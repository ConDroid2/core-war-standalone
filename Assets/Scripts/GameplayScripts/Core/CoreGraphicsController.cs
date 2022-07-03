using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoreGraphicsController : MonoBehaviour
{
    [SerializeField] private Material neutralMat;
    [SerializeField] private Material primedByMeMat;
    [SerializeField] private Material primedByEnemyMat;

    [SerializeField] private TextMeshPro influenceText;

    Core core;

    // Start is called before the first frame update
    private void Awake()
    {
        core = GetComponent<Core>();
        core.OnInfluenceChanged += HandleCoreInfluenceChange;
        HandleCoreInfluenceChange(0);
    }

    private void OnDestroy()
    {
        core.OnInfluenceChanged -= HandleCoreInfluenceChange;
    }

    public void HandleCoreInfluenceChange(int newAmount)
    {
        if(newAmount > 0)
        {
            core.renderer.material = primedByMeMat;
        }
        else if(newAmount < 0)
        {
            core.renderer.material = primedByEnemyMat;
        }
        else
        {
            core.renderer.material = neutralMat;
        }

        influenceText.text = newAmount.ToString();
    }
}

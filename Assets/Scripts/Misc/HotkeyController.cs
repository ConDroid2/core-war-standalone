using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyController : MonoBehaviour
{
    [SerializeField] private GameObject GameInfoUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameInfoUI.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            GameInfoUI.SetActive(false);
        }
    }
}

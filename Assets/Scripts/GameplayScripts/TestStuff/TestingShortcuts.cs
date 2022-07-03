using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingShortcuts : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if (!DevConfigs.IsDevBuild)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MagickManager.Instance.ChangeLevel("Blue", 1);
            MagickManager.Instance.ChangeCurrent("Blue", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MagickManager.Instance.ChangeLevel("Red", 1);
            MagickManager.Instance.ChangeCurrent("Red", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MagickManager.Instance.ChangeLevel("Green", 1);
            MagickManager.Instance.ChangeCurrent("Green", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            MagickManager.Instance.ChangeLevel("Black", 1);
            MagickManager.Instance.ChangeCurrent("Black", 1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Player.Instance.StartTurn();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            NotificationManager.Instance.FireNotification("This is a test");
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            IgniteManager.Instance.IncreaseIgniteCount(1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MainSequenceManager.Instance.Add(Player.Instance.draw);
        }
        //else if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    Debug.Log("Current Action in sequence: " + MainSequenceManager.Instance.mainSequence.currentAction.GetType());
        //}
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SequenceSystem.DiscardFromTopOfDeck discard = new SequenceSystem.DiscardFromTopOfDeck(3);
            MainSequenceManager.Instance.Add(discard);
        }
    }
}

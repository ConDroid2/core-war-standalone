using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTimer : MonoBehaviour
{
    [SerializeField] Image graphic;
    [SerializeField] Button button;

    private float totalTime = 1f;
    private float timer = 0f;
    private bool active = false;

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                EndTimer();
                button.onClick.Invoke();
            }
        }

        graphic.fillAmount = timer / totalTime;
    }

    public void Pause()
    {
        active = false; 
    }

    public void UnPause()
    {
        active = true;
    }

    public void StartTimer(float time)
    {
        totalTime = time;
        timer = time;
        active = true;
    }

    public void EndTimer()
    {
        active = false;
        timer = 0f;
    }
}

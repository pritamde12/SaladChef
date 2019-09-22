using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Customer : MonoBehaviour
{
    public UnityAction OnTimeUp;

    public Image timeFillBar;

    public float totalTime;
    public float currentTime;

    bool canStart = false;
    float angryMode = 1;
    private void Update()
    {
        if (!canStart)
            return;

        currentTime += Time.deltaTime * angryMode;

        var percent = currentTime / totalTime;

        timeFillBar.fillAmount = Mathf.Lerp(0, 1, percent);

        if (timeFillBar.fillAmount== 1)
        {
            canStart = false;

            OnTimeUp?.Invoke();
        }
    }

    public void SetTimer(float t)
    {
        totalTime  = t;
        currentTime = 0;
        angryMode = 1;
    }

    public void StartTimer()
    {
        canStart = true;        
    }

    public void StopTimer()
    {
        canStart = false;
        currentTime = 0;
    }

    public void AngryModeOn()
    {
        angryMode = 2;
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}

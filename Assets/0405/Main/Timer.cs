using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timer;
    [SerializeField]
    private float gametime = 1.0f;
    private float nowTime;
    public void SetMaxTime() { nowTime = gametime; }
    public float GetNowTime() { return nowTime; }

    private void Awake()
    {
        SetMaxTime();
        timer.text = nowTime.ToString("F1");
    }

    private void Update()
    {
        if (nowTime > 0)
        {
            nowTime -= Time.deltaTime;
        }
        else { nowTime = 0; }

        // è¨êî1åÖÇ…ÇµÇƒï\é¶
        timer.text = nowTime.ToString("F1");
    }

    public float getTime()
    {
        return nowTime;
    }
}

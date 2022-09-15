using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timer;
    [SerializeField]
    private float gametime = 1.0f;

    private void Update()
    {
        if (gametime > 0)
        {
            gametime -= Time.deltaTime;
        }
        else { gametime = 0; }

        // ¬”1Œ…‚É‚µ‚Ä•\¦
        timer.text = gametime.ToString("F1");
    }

    public float getTime()
    {
        return gametime;
    }
}

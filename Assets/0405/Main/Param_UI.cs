using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Param_UI : MonoBehaviour
{
    [SerializeField]
    private PlayerStatus playerstatus;
    [SerializeField]
    private Text powerText, defenceText, speedText;

    void Start()
    {
        //初期値を設定
        powerTextUpdate(playerstatus.GetPower());
        defenceTextUpdate(playerstatus.GetDefense());
        speedTextUpdate(playerstatus.GetSpeed());
    }

    private void Update()
    {
        powerTextUpdate(playerstatus.GetPower());
        defenceTextUpdate(playerstatus.GetDefense());
        speedTextUpdate(playerstatus.GetSpeed());
    }
    public void powerTextUpdate(float power)
    {
        powerText.text = power.ToString("F0");
    }
    public void defenceTextUpdate(float defence)
    {
        defenceText.text = defence.ToString("F0");
    }
    public void speedTextUpdate(float speed)
    {
        speedText.text = speed.ToString("F0");
    }
}
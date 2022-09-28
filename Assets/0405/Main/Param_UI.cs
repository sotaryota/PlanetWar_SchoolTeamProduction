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
        //èâä˙ílÇê›íË
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
        powerText.text = power.ToString();
    }
    public void defenceTextUpdate(float defence)
    {
        defenceText.text = defence.ToString();
    }
    public void speedTextUpdate(float speed)
    {
        speedText.text = speed.ToString();
    }
}
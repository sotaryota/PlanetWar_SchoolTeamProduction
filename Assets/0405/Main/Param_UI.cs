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
        //‰Šú’l‚ğİ’è
        PowerTextUpdate(playerstatus.GetPower());
        defenceTextUpdate(playerstatus.GetDefense());
        speedTextUpdate(playerstatus.GetSpeed());
    }

    public void PowerTextUpdate(float power)
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
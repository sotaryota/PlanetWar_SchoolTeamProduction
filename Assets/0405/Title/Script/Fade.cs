using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image image;

    //falseで透明 trueで黒くなる
    public bool fademode;

    void Start()
    {
        image.color = Color.black;

        //falseで透明 trueで黒くなる
        fademode = false;
    }

    void Update()
    {
        float alpha = image.color.a;
        if (fademode && alpha < 1)
        {
            alpha += Time.deltaTime;
        }
        else if(alpha > 0)
        {
            alpha -= Time.deltaTime;
        }
        image.color = new Color(0, 0, 0, alpha);
    }
    
    //フェードアウトが終わったらTrueを返す
    //シーンの終わりに使う
    public bool FadeOut()
    {
        float alpha = image.color.a;
        return fademode && alpha >= 0.95f;
    }

    //フェードインが終わったらTrueを返す
    //シーンの初めに使う
    public bool FadeIn()
    {
        float alpha = image.color.a;
        return !fademode && alpha <= 0.05f;
    }
}

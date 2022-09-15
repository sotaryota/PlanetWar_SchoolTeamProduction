using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image image;
    public bool fademode;

    // Start is called before the first frame update
    void Start()
    {
        image.color = Color.black;
        fademode = false;
    }

    // Update is called once per frame
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

    public bool FadeOut()
    {
        float alpha = image.color.a;
        return fademode && alpha >= 0.95f;
    }

    public bool FadeIn()
    {
        float alpha = image.color.a;
        return !fademode && alpha <= 0.05f;
    }
}

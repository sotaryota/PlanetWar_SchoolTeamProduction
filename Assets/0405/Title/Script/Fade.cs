using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image image;

    //false�œ��� true�ō����Ȃ�
    public bool fademode;

    void Start()
    {
        image.color = Color.black;

        //false�œ��� true�ō����Ȃ�
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
    
    //�t�F�[�h�A�E�g���I�������True��Ԃ�
    //�V�[���̏I���Ɏg��
    public bool FadeOut()
    {
        float alpha = image.color.a;
        return fademode && alpha >= 0.95f;
    }

    //�t�F�[�h�C�����I�������True��Ԃ�
    //�V�[���̏��߂Ɏg��
    public bool FadeIn()
    {
        float alpha = image.color.a;
        return !fademode && alpha <= 0.05f;
    }
}

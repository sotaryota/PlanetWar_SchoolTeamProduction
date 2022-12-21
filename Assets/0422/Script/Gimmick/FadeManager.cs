using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    private Color color;         //�t�F�[�h�A�E�g�̐F
    private float alpha = 0f;    //�����x
    private bool isFade = false; //�t�F�[�h���Ă��邩�ǂ���

    private void OnGUI()
    {
        if (isFade)
        {
            //�����x��alpha�̒l��
            color.a = alpha;

            //GUI�ŕ\��
            GUI.color = color;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }
    public void SceneFadeIn(float speed)
    {
        StartCoroutine(FadeIn(speed));
    }

    /// <summary>
    /// �V�[����؂�ւ��������ɌĂяo��
    /// RGB�ŃJ���[�w�肵�ڍs�������V�[�����������ɂ��ăt�F�[�h�A�E�g
    /// </summary>
    /// <param name="scene">�V�[����</param>
    /// <param name="r">��</param>
    /// <param name="g">��</param>
    /// <param name="b">��</param>
    /// <param name="interval">�t�F�[�h�̃X�s�[�h</param>
    public void FadeSceneChange(string scene, float r, float g, float b, float interval)
    {
        color = new Color(r, g, b);
        StartCoroutine(FadeOut(scene, interval));
    }
    /// <summary>
    /// �Q�[���I�����ɌĂяo��
    /// RGB�ŃJ���[�w�肵�ڍs�������V�[�����������ɂ��ăt�F�[�h�A�E�g
    /// </summary>
    /// <param name="r">��</param>
    /// <param name="g">��</param>
    /// <param name="b">��</param>
    /// <param name="interval">�t�F�[�h�̃X�s�[�h</param>
    public void GameEndFadeOut(float r, float g, float b, float interval)
    {
        color = new Color(r, g, b);
        StartCoroutine(GameEndFade(interval));
    }

    //-----------------------------------------------------
    //�֐����ŌĂяo����鏈��
    //-----------------------------------------------------

    private IEnumerator FadeIn(float speed)
    {
        isFade = true;

        //�t�F�[�h�̃J�E���g
        float fadetime = 0;

        //�t�F�[�h�A�E�g
        while (fadetime <= speed)
        {
            //�����x���������グ��
            alpha = Mathf.Lerp(1f, 0f, fadetime / speed);
            fadetime += Time.deltaTime;
            yield return 0;
        }
    }

    private IEnumerator FadeOut(string scene, float speed)
    {
        isFade = true;

        //�t�F�[�h�̃J�E���g
        float fadetime = 0;

        //�t�F�[�h�A�E�g
        while (fadetime <= speed)
        {
            //�����x���������グ��
            alpha = Mathf.Lerp(0f, 1f, fadetime / speed);
            fadetime += Time.deltaTime;
            yield return 0;
        }

        //�t�F�[�h�C�����I������V�[���؂�ւ�
        SceneManager.LoadScene(scene);
    }
    private IEnumerator GameEndFade(float speed)
    {
        isFade = true;

        //�t�F�[�h�̃J�E���g
        float fadetime = 0;

        //�t�F�[�h�A�E�g
        while (fadetime <= speed)
        {
            //�����x���������グ��
            alpha = Mathf.Lerp(0f, 1f, fadetime / speed);
            fadetime += Time.deltaTime;
            yield return 0;
        }

        //�t�F�[�h�C�����I��������Q�[���I��
        Application.Quit();
    }
}
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

    /// <summary>
    /// �V�[����؂�ւ��������ɌĂяo��
    /// RGB�ŃJ���[�w�肵�ڍs�������V�[�����������ɂ��ăt�F�[�h�A�E�g
    /// </summary>
    /// <param name="scene">�V�[����</param>
    /// <param name="r">��</param>
    /// <param name="g">��</param>
    /// <param name="b">��</param>
    /// <param name="interval">�t�F�[�h�̃X�s�[�h</param>
    public void FadeOut(string scene, float r, float g, float b, float interval)
    {
        color = new Color(r, g, b);
        StartCoroutine(Fade(scene, interval));
    }

    //-----------------------------------------------------
    //�֐����ŌĂяo����鏈��
    //-----------------------------------------------------

    private IEnumerator Fade(string scene, float speed)
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
}
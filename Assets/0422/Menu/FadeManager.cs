using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    private Color color;         //フェードアウトの色
    private float alpha = 0f;    //透明度
    private bool isFade = false; //フェードしているかどうか

    private void OnGUI()
    {
        if (isFade)
        {
            //透明度をalphaの値に
            color.a = alpha;

            //GUIで表示
            GUI.color = color;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    /// <summary>
    /// シーンを切り替えたい時に呼び出す
    /// RGBでカラー指定し移行したいシーン名を引数にしてフェードアウト
    /// </summary>
    /// <param name="scene">シーン名</param>
    /// <param name="r">赤</param>
    /// <param name="g">緑</param>
    /// <param name="b">青</param>
    /// <param name="interval">フェードのスピード</param>
    public void FadeOut(string scene, float r, float g, float b, float interval)
    {
        color = new Color(r, g, b);
        StartCoroutine(Fade(scene, interval));
    }

    //-----------------------------------------------------
    //関数内で呼び出される処理
    //-----------------------------------------------------

    private IEnumerator Fade(string scene, float speed)
    {
        isFade = true;

        //フェードのカウント
        float fadetime = 0;

        //フェードアウト
        while (fadetime <= speed)
        {
            //透明度を少しずつ上げる
            alpha = Mathf.Lerp(0f, 1f, fadetime / speed);
            fadetime += Time.deltaTime;
            yield return 0;
        }

        //フェードインが終了次第シーン切り替え
        SceneManager.LoadScene(scene);
    }
}
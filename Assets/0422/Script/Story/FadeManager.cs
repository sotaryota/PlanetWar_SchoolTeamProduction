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
    /// バトルからストーリーに移行した際に呼ぶ関数
    /// </summary>
    /// <param name="player">プレイヤーのゲームオブジェクト</param>
    /// <param name="data">保存されたデータ</param>
    /// <param name="r">赤</param>
    /// <param name="g">緑</param>
    /// <param name="b">青</param>
    /// <param name="speed">フェードのスピード</param>
    /// <param name="interval">フェード開始までのインターバル</param>
    public void ToStorySceneFadeIn(ref GameObject player,PlayerDataManager data,float r, float g, float b, float speed, float interval)
    {
        color = new Color(r, g, b);
        StartCoroutine(ToStoryFadeIn(player,data,speed, interval));
    }
    /// <summary>
    /// シーン開始時のフェードイン時に呼び出す
    /// </summary>
    /// <param name="r">赤</param>
    /// <param name="g">緑</param>
    /// <param name="b">青</param>
    /// <param name="speed">フェードのスピード</param>
    /// <param name="interval">フェード開始までのインターバル</param>
    public void SceneFadeIn(float r, float g, float b, float speed, float interval)
    {
        color = new Color(r, g, b);
        StartCoroutine(FadeIn(speed,interval));
    }

    /// <summary>
    /// シーンを切り替えたい時に呼び出す
    /// RGBでカラー指定し移行したいシーン名を引数にしてフェードアウト
    /// </summary>
    /// <param name="scene">シーン名</param>
    /// <param name="r">赤</param>
    /// <param name="g">緑</param>
    /// <param name="b">青</param>
    /// <param name="speed">フェードのスピード</param>
    public void FadeSceneChange(string scene, float r, float g, float b, float speed)
    {
        color = new Color(r, g, b);
        StartCoroutine(FadeOut(scene, speed));
    }

    /// <summary>
    /// ゲーム終了時に呼び出す
    /// RGBでカラー指定し移行したいシーン名を引数にしてフェードアウト
    /// </summary>
    /// <param name="r">赤</param>
    /// <param name="g">緑</param>
    /// <param name="b">青</param>
    /// <param name="speed">フェードのスピード</param>
    public void GameEndFadeOut(float r, float g, float b, float speed)
    {
        color = new Color(r, g, b);
        StartCoroutine(GameEndFade(speed));
    }

    //-----------------------------------------------------
    //関数内で呼び出される処理
    //-----------------------------------------------------

    private IEnumerator ToStoryFadeIn(GameObject player, PlayerDataManager data,float speed, float interval)
    {
        isFade = true;
        alpha = 1f;
        float time = 0;
        //フェードイン
        while (time <= interval)
        {
            player.transform.position = data.playerPos;
            time += Time.deltaTime;
            yield return 0;
        }

        //フェードのカウント
        float fadetime = 0;

        //フェードイン
        while (fadetime <= speed)
        {
            //透明度を少しずつ上げる
            alpha = Mathf.Lerp(1f, 0f, fadetime / speed);
            fadetime += Time.deltaTime;
            yield return 0;
        }
    }
    private IEnumerator FadeIn(float speed,float interval)
    {
        isFade = true;
        alpha = 1f;

        yield return new WaitForSeconds(interval);

        //フェードのカウント
        float fadetime = 0;

        //フェードイン
        while (fadetime <= speed)
        {
            //透明度を少しずつ上げる
            alpha = Mathf.Lerp(1f, 0f, fadetime / speed);
            fadetime += Time.deltaTime;
            yield return 0;
        }
    }

    private IEnumerator FadeOut(string scene, float speed)
    {
        isFade = true;

        //フェードのカウント
        float fadetime = 0;

        //フェードアウト
        while (fadetime <= speed)
        {
            //透明度を少しずつ下げる
            alpha = Mathf.Lerp(0f, 1f, fadetime / speed);
            fadetime += Time.deltaTime;
            yield return 0;
        }

        //フェードインが終了次第シーン切り替え
        SceneManager.LoadScene(scene);
    }
    private IEnumerator GameEndFade(float speed)
    {
        isFade = true;

        //フェードのカウント
        float fadetime = 0;

        //フェードアウト
        while (fadetime <= speed)
        {
            //透明度を少しずつ下げる
            alpha = Mathf.Lerp(0f, 1f, fadetime / speed);
            fadetime += Time.deltaTime;
            yield return 0;
        }

        //フェードインが終了したらゲーム終了
        Application.Quit();
    }
}
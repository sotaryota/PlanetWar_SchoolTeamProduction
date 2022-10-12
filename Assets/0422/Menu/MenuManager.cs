using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public enum SelectMenu
    {
        Start,      //スタート
        Tutorial,   //チュートリアル
        Title,      //タイトル
        End,        //ゲーム終了
    };

    [System.Serializable]
    public class MenuData
    {
        public GameObject menuImage;    //表示する画像
        public GameObject selectPlanet; //選択中のメニューに対応した惑星
        public string sceneName;        //移行したいシーンの名前
        public float effectSize;        //エフェクトの大きさ 
    };

    [SerializeField] 
    private PlanetRotate planetRotate;
    [SerializeField]
    private float lockValue;
    [SerializeField]
    private MenuSEManager menuSE;
    [SerializeField]
    private GameObject effectPrefab;
    [SerializeField]
    private FadeManager fade;
    private Gamepad gamepad;
    public MenuData[] menuDatas;
    public SelectMenu nowSelect;    //現在選択されているメニュー
    public SelectMenu beforeSelect; //選択中のメニューを一時保存

    private void Start()
    {
        nowSelect = SelectMenu.Start;
        beforeSelect = nowSelect;
    }

    private void Update()
    {
        if(gamepad == null)
        {
            gamepad = Gamepad.current;
        }

        DecisionScene();
    }

    /// <summary>
    /// ボタンを押したときに選んでいる惑星の位置に
    /// 爆発のエフェクトを出してシーン移行する
    /// </summary>
    public void DecisionScene()
    {
        if(planetRotate.buttonLock)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                menuDatas[(int)nowSelect].selectPlanet.SetActive(false);

                GameObject effect = Instantiate(effectPrefab);

                //エフェクトのサイズとポジションを指定
                effect.transform.localScale = new Vector3(menuDatas[(int)nowSelect].effectSize,
                    menuDatas[(int)nowSelect].effectSize, menuDatas[(int)nowSelect].effectSize);
                effect.transform.position = menuDatas[(int)nowSelect].selectPlanet.transform.position;

                menuSE.DecisionSE();

                //シーン切り替え
                StartCoroutine("SceneChange", menuDatas[(int)nowSelect].sceneName);
            }
        }
    }


    [SerializeField] private float fadeInterval; // フェードまでの間隔
    [SerializeField] private float fadeSpeed;    // フェードのスピード
    [SerializeField] private Color fadeColor;    // フェードのカラー

    IEnumerator SceneChange(string sceneName)
    {
        planetRotate.buttonLock = false;

        yield return new WaitForSeconds(fadeInterval);

        if (sceneName != "End")
        {
            fade.FadeOut(sceneName, fadeColor.r, fadeColor.g, fadeColor.b, fadeSpeed);
        }
        else
        {
            Application.Quit();
        }
    }
}

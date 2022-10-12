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
    };

    [SerializeField] 
    private PlanetRotate planetRotate;
    [SerializeField]
    private float lockValue;
    [SerializeField]
    private MenuSEManager menuSE;
    [SerializeField]
    private GameObject effectPrefab;
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
    /// メニューの切り替えをしていない時
    /// ボタンを押すと引数の名前のシーンに移行
    /// Endの場合はゲーム終了
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    public void DecisionScene()
    {
        if(planetRotate.buttonLock)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                menuDatas[(int)nowSelect].selectPlanet.SetActive(false);
                GameObject effect = Instantiate(effectPrefab);
                effect.transform.position = menuDatas[(int)nowSelect].selectPlanet.transform.position;
                StartCoroutine("SceneChange", menuDatas[(int)nowSelect].sceneName);
            }
        }
    }


    [SerializeField]
    private float decisionWait;

    IEnumerator SceneChange(string sceneName)
    {
        yield return new WaitForSeconds(decisionWait);

        if (sceneName != "End")
        {
            menuSE.DecisionSE();
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            menuSE.DecisionSE();
            Application.Quit();
        }
    }
}

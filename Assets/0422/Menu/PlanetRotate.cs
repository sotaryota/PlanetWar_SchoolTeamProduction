using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetRotate : MonoBehaviour
{
    Gamepad gamepad;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject PlanetGameObject;
    [SerializeField] private MenuSEManager menuSE;
    public bool buttonLock;  //選択中にカーソルを動かせないように

    private void Awake()
    {
        buttonLock = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(gamepad == null)
        {
            gamepad = Gamepad.current;
        }

        PlanetRotation();
    }

    //--------------------------------------
    //カメラを中心に置き四方の惑星を回転させる
    //--------------------------------------

    public void PlanetRotation()
    {
        if (buttonLock)
        {
            //右入力
            if (gamepad.leftStick.ReadValue().x > 0)
            {
                StartCoroutine("RightRotation");
            }
            //左入力
            else if (gamepad.leftStick.ReadValue().x < 0)
            {
                StartCoroutine("LeftRotation");
            }
        }
    }

    //--------------------------------------
    //回転処理
    //--------------------------------------

    //回転時間
    [SerializeField] private float rotateTime; 

    //右回転
    IEnumerator RightRotation()
    {
        //回転をロック
        buttonLock = false;
        
        //効果音
        menuSE.SelectSE();
        
        //回転をループさせるif
        if (menuManager.nowSelect >= MenuManager.SelectMenu.End)
        {
            menuManager.nowSelect = MenuManager.SelectMenu.Start;
        }
        else
        {
            menuManager.nowSelect += 1;
        }

        //表示されていた画像を消す
        menuManager.menuDatas[(int)menuManager.beforeSelect].menuImage.SetActive(false);

        //惑星の回転、rotateTimeの時間で回転速度を変更
        for (int i = 0; i < 45; ++i)
        {
            PlanetGameObject.transform.Rotate(0,-2,0);
            yield return new WaitForSeconds(1 / (rotateTime * 60));
        }

        //選択したメニューに応じた画像を表示
        menuManager.menuDatas[(int)menuManager.nowSelect].menuImage.SetActive(true);

        //現在選択中のメニューを保存
        menuManager.beforeSelect = menuManager.nowSelect;

        //ロック解除
        buttonLock = true;
    }

    //左回転
    IEnumerator LeftRotation()
    {
        //回転をロック
        buttonLock = false;

        //効果音
        menuSE.SelectSE();

        //回転をループさせるif
        if (menuManager.nowSelect <= MenuManager.SelectMenu.Start)
        {
            menuManager.nowSelect = MenuManager.SelectMenu.End;
        }
        else
        {
            menuManager.nowSelect -= 1;
        }

        //表示されていた画像を消す
        menuManager.menuDatas[(int)menuManager.beforeSelect].menuImage.SetActive(false);

        //惑星の回転、rotateTimeの時間で回転速度を変更
        for (int i = 0; i < 45; ++i)
        {
            PlanetGameObject.transform.Rotate(0, 2, 0);
            yield return new WaitForSeconds(1 / (rotateTime * 60));
        }

        //選択したメニューに応じた画像を表示
        menuManager.menuDatas[(int)menuManager.nowSelect].menuImage.SetActive(true);

        //現在選択中のメニューを保存
        menuManager.beforeSelect = menuManager.nowSelect;

        //ロック解除
        buttonLock = true;
    }
}

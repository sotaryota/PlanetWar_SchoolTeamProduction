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

    //--------------------------------------------------------------
    //回転処理
    //for文だと回転スピードがスペック依存になるので回転方法を模索中
    //--------------------------------------------------------------

    [Header("惑星の回転")]
    [SerializeField] private float rotateTime;  //回転時間
    [SerializeField] private float rotateAngle; //回転量

    //右回転
    IEnumerator RightRotation()
    {
        buttonLock = false;
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

        //惑星の回転
        for (int i = 0; i < (90 / rotateAngle); ++i)
        {
            PlanetGameObject.transform.Rotate(0,-rotateAngle, 0);

            //rotateTimeの値が1なら等速、大きくなる程速く回転する
            yield return new WaitForSeconds(1 / (rotateTime * 60));
        }

        //選択したメニューに応じた画像を表示
        menuManager.menuDatas[(int)menuManager.nowSelect].menuImage.SetActive(true);

        //現在選択中のメニューを保存
        menuManager.beforeSelect = menuManager.nowSelect;

        buttonLock = true;
    }

    //左回転
    IEnumerator LeftRotation()
    {
        buttonLock = false;
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

        //惑星の回転
        for (int i = 0; i < (90 / rotateAngle); ++i)
        {
            //1フレーム毎の回転
            PlanetGameObject.transform.Rotate(0, rotateAngle, 0);

            //rotateTimeの値が1なら等速、大きくなる程速く回転する
            yield return new WaitForSeconds(1 / (rotateTime * 60));
        }

        //選択したメニューに応じた画像を表示
        menuManager.menuDatas[(int)menuManager.nowSelect].menuImage.SetActive(true);

        //現在選択中のメニューを保存
        menuManager.beforeSelect = menuManager.nowSelect;

        buttonLock = true;
    }
}

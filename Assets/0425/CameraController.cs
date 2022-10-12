using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CameraController : MonoBehaviour
{
    Gamepad gamepad;
    TestInputActions testInputActions;
    public PlayerStatus playerStatus;
    int playerID;

    public GameObject player;
    public GameObject cameraController; 

    float horizontal;
    float vertical;

    [SerializeField] float rotateSpeed = 1.0f;  //カメラの回転速度

    public GameObject target;  //ロックオン対象
    bool lockOn1P = false;  //ロックオン中か否か（1P）
    bool lockOn2P = false;  //ロックオン中か否か（2P）

    //ズーム処理用(仮)
    bool gekihaSceneFlag = false;
   
    void Start()
    {
        testInputActions = new TestInputActions();
        testInputActions.Enable();

       transform.position = player.transform.position;

        playerID = playerStatus.GetID();  //プレイヤーIDを取得
    }

    void Update()
    {
        if (gamepad == null)
        {
            gamepad = Gamepad.all[playerID];
        }

        StickValue();
        rotateCmaeraAngle();

        //ロックオン-------------------------------------------------------------------------------
   
        //R1ボタンでロックオンする
        ButtonControl lockOnButton = gamepad[UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder];

        //1P
        if (playerID == 0) 
        {  
            //R1ボタンが押されたら
            if (lockOnButton.wasPressedThisFrame)
            {
                //ロックオン状態を切り替える
                LockOn(); 
            }
            if (lockOn1P)
            {
                cameraController.transform.LookAt(target.transform.position); //ロックオン対象の方向に向き続ける
            }
        }
        //2P
        else if (playerID == 1) 
        {
            //R1ボタンが押されたら
            if (lockOnButton.wasPressedThisFrame) 
            {
                print($"[{UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder}] 2PisPressed = {lockOnButton.isPressed}");
                //ロックオン状態を切り替える
                LockOn(); 
            }
            if (lockOn2P)
            {
                cameraController.transform.LookAt(target.transform.position); //ロックオン対象の方向に向き続ける
            }
        }

        //カメラの追従
        //transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 5.0f);


        //ズーム処理お試し-----------------------------------------------------------------------------------------
        ButtonControl gekihaScene = gamepad[UnityEngine.InputSystem.LowLevel.GamepadButton.LeftShoulder]; //L1ボタン

        //Flagを切り替え
        if (gekihaScene.wasPressedThisFrame)
        {
            if (gekihaSceneFlag)
                gekihaSceneFlag = false;
            else
                gekihaSceneFlag = true;
        }
        if (gekihaSceneFlag == false)
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.1f); //自分に追従
        else
        transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.1f); //相手にズーム
        //-----------------------------------------------------------------------------------------------------------
    }

    void LockOn()
    {
        if (playerID == 0) //1P
        {
            if (lockOn1P == false)
                lockOn1P = true;
            else
                lockOn1P = false;
        }
        else
        if (playerID == 1) //2P
        {
            if (lockOn2P == false)
                lockOn2P = true;
            else
                lockOn2P = false;
        }
    } 


    private void StickValue()
    {
        horizontal = gamepad.rightStick.x.ReadValue();
        vertical = gamepad.rightStick.y.ReadValue();
    }

    //カメラの回転
    private void rotateCmaeraAngle()
    {
        transform.eulerAngles += new Vector3(0, horizontal * rotateSpeed * Time.deltaTime);
    }   
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System.Collections;

public class CameraController : MonoBehaviour
{
    Gamepad gamepad;
    TestInputActions testInputActions;
    public PlayerStatus playerStatus;
    int playerID;

    public Camera playerCamera;

    [SerializeField] AudioSource audioSource;
    //[SerializeField] AudioClip audioClip;

    public GameObject player;
    public GameObject cameraController;

    [SerializeField]
    GameObject rockOnImage;
    [SerializeField]
    GameObject rockOnPos;

    float horizontal;
    float vertical;

    [SerializeField] float rotateSpeed = 1.0f;  //カメラの回転速度

    public GameObject target;  //ロックオン対象
    bool lockOn1P = false;  //ロックオン中か否か（1P）
    bool lockOn2P = false;  //ロックオン中か否か（2P）

    //ズーム処理用(仮)
    bool SettlementSceneFlag = false;

    public PhotonView myPV;

    void Start()
    {
        testInputActions = new TestInputActions();
        testInputActions.Enable();

        transform.position = player.transform.position;

        playerID = playerStatus.GetID();  //プレイヤーIDを取得

        audioSource = GetComponent<AudioSource>();

        rockOnImage.SetActive(false);
    }

    void Update()
    {
        if (myPV.isMine)    //自キャラであれば実行
        {
            if (gamepad == null)
            {
                gamepad = Gamepad.all[playerStatus.GetID()];
            }
        }

        //ロックオン画像の位置を変更
        Vector3 targetViewportPoint = playerCamera.WorldToViewportPoint(rockOnPos.transform.position);
        targetViewportPoint.z = 0;
        Vector3 screen = RectTransformUtility.WorldToScreenPoint(playerCamera, rockOnPos.transform.position);
        rockOnImage.transform.position =screen;
        //R1ボタンでロックオンする
        ButtonControl lockOnButton = gamepad[UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder];

        //1P
        if (playerID == 0)
        {
            //R1ボタンが押されたら
            if (lockOnButton.wasPressedThisFrame)
            {
                print($"[{UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder}] 1PisPressed = {lockOnButton.isPressed}");
                audioSource.Play();
                //ロックオン状態を切り替える
                LockOn();
            }
            if (lockOn1P)
            {
                cameraController.transform.LookAt(target.transform.position); //ロックオン対象の方向に向き続ける
            }
            else
            {
                StickValue();
                rotateCameraAngle();
            }
        }
        //2P
        else if (playerID == 1)
        {
            //R1ボタンが押されたら
            if (lockOnButton.wasPressedThisFrame)
            {
                print($"[{UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder}] 2PisPressed = {lockOnButton.isPressed}");
                audioSource.Play();
                //ロックオン状態を切り替える
                LockOn();
            }
            if (lockOn2P)
            {
                cameraController.transform.LookAt(target.transform.position); //ロックオン対象の方向に向き続ける
            }
            else
            {
                StickValue();
                rotateCameraAngle();
            }
        }

        //カメラの追従
        //transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 5.0f);


        /*ズーム処理お試し-----------------------------------------------------------------------------------------
        ButtonControl SettlementScene = gamepad[UnityEngine.InputSystem.LowLevel.GamepadButton.LeftShoulder]; //L1ボタン

        //Flagを切り替え
        if (SettlementScene.wasPressedThisFrame)
        {
            if (SettlementSceneFlag)
            {
                SettlementSceneFlag = false;

            }
            else
            {
                SettlementSceneFlag = true;
            }
        }

        */
        //-----------------------------------------------------------------------------------------------------------
    }

    private void FixedUpdate()
    {
        if (SettlementSceneFlag == false)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.1f); //自分に追従
        }
        else
        {
            //transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.1f); //相手にズーム
        }
    }
    void LockOn()
    {
        if (playerID == 0) //1P
        {
            if (lockOn1P == false)
            {
                rockOnImage.SetActive(true);
                lockOn1P = true;
            }
            else
            {
                rockOnImage.SetActive(false);
                rockOnImage.transform.position = new Vector3(-1000, -1000, -1000);
                lockOn1P = false;
            }
        }
        else
        if (playerID == 1) //2P
        {
            if (lockOn2P == false)
            {
                rockOnImage.SetActive(true);
                lockOn2P = true;
            }
            else
            {
                rockOnImage.SetActive(false);
                rockOnImage.transform.position = new Vector3(-1000, -1000, -1000);
                lockOn2P = false;
            }
        }
    }


    private void StickValue()
    {
        horizontal = gamepad.rightStick.x.ReadValue();
        vertical = gamepad.rightStick.y.ReadValue();
    }

    //カメラの回転
    private void rotateCameraAngle()
    {
        transform.eulerAngles += new Vector3(0, horizontal * rotateSpeed * Time.deltaTime);
    }
}

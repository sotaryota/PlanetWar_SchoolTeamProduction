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

    [SerializeField] float rotateSpeed = 1.0f;  //�J�����̉�]���x

    public GameObject target;  //���b�N�I���Ώ�
    bool lockOn1P = false;  //���b�N�I�������ۂ��i1P�j
    bool lockOn2P = false;  //���b�N�I�������ۂ��i2P�j

    //�Y�[�������p(��)
    bool SettlementSceneFlag = false;

    public PhotonView myPV;

    void Start()
    {
        testInputActions = new TestInputActions();
        testInputActions.Enable();

        transform.position = player.transform.position;

        playerID = playerStatus.GetID();  //�v���C���[ID���擾

        audioSource = GetComponent<AudioSource>();

        rockOnImage.SetActive(false);
    }

    void Update()
    {
        if (myPV.isMine)    //���L�����ł���Ύ��s
        {
            if (gamepad == null)
            {
                gamepad = Gamepad.all[playerStatus.GetID()];
            }
        }

        //���b�N�I���摜�̈ʒu��ύX
        Vector3 targetViewportPoint = playerCamera.WorldToViewportPoint(rockOnPos.transform.position);
        targetViewportPoint.z = 0;
        Vector3 screen = RectTransformUtility.WorldToScreenPoint(playerCamera, rockOnPos.transform.position);
        rockOnImage.transform.position =screen;
        //R1�{�^���Ń��b�N�I������
        ButtonControl lockOnButton = gamepad[UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder];

        //1P
        if (playerID == 0)
        {
            //R1�{�^���������ꂽ��
            if (lockOnButton.wasPressedThisFrame)
            {
                print($"[{UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder}] 1PisPressed = {lockOnButton.isPressed}");
                audioSource.Play();
                //���b�N�I����Ԃ�؂�ւ���
                LockOn();
            }
            if (lockOn1P)
            {
                cameraController.transform.LookAt(target.transform.position); //���b�N�I���Ώۂ̕����Ɍ���������
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
            //R1�{�^���������ꂽ��
            if (lockOnButton.wasPressedThisFrame)
            {
                print($"[{UnityEngine.InputSystem.LowLevel.GamepadButton.RightShoulder}] 2PisPressed = {lockOnButton.isPressed}");
                audioSource.Play();
                //���b�N�I����Ԃ�؂�ւ���
                LockOn();
            }
            if (lockOn2P)
            {
                cameraController.transform.LookAt(target.transform.position); //���b�N�I���Ώۂ̕����Ɍ���������
            }
            else
            {
                StickValue();
                rotateCameraAngle();
            }
        }

        //�J�����̒Ǐ]
        //transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 5.0f);


        /*�Y�[������������-----------------------------------------------------------------------------------------
        ButtonControl SettlementScene = gamepad[UnityEngine.InputSystem.LowLevel.GamepadButton.LeftShoulder]; //L1�{�^��

        //Flag��؂�ւ�
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
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.1f); //�����ɒǏ]
        }
        else
        {
            //transform.position = Vector3.Lerp(transform.position, target.transform.position, 0.1f); //����ɃY�[��
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

    //�J�����̉�]
    private void rotateCameraAngle()
    {
        transform.eulerAngles += new Vector3(0, horizontal * rotateSpeed * Time.deltaTime);
    }
}

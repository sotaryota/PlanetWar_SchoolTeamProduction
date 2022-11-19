using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera_Solo : MonoBehaviour
{
    Gamepad gamepad;
    TestInputActions testInputActions;
    [Header("�v���C���[")]
    [SerializeField] private PlayerStatus_Solo playerStatus;
    [SerializeField] private GameObject player;
    [Header("�J����")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject cameraController;
    [SerializeField] float rotateSpeed;  //�J�����̉�]���x
    [SerializeField] float followSpeed;  //�J�����̒Ǐ]���x

    float horizontal;
    float vertical;

    [SerializeField]
    float angleUpLimit = 50f; //�J������Y�����̏��
    [SerializeField]
    float angleDownLimit = -40f; //�J������Y�����̉���

    void Start()
    {
        testInputActions = new TestInputActions();
        testInputActions.Enable();

        transform.position = player.transform.position;
    }

    void Update()
    {
        if (gamepad == null) { gamepad = Gamepad.current; }

        //�J�����̏㉺�̊p�x����
        float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(angle_x, angleDownLimit, angleUpLimit),
            transform.eulerAngles.y,
            transform.eulerAngles.z
        );

        StickValue();
        rotateCameraAngle();

        transform.position = Vector3.Lerp(transform.position, player.transform.position, followSpeed); //�����ɒǏ]
    }

    //private void FixedUpdate()
    //{
    //    transform.position = Vector3.Lerp(transform.position, player.transform.position, followSpeed); //�����ɒǏ]
    //}

    private void StickValue()
    {
        horizontal = gamepad.rightStick.x.ReadValue();
        vertical = gamepad.rightStick.y.ReadValue();
    }

    //�J�����̉�]
    private void rotateCameraAngle()
    {
        //transform.eulerAngles += new Vector3(0, horizontal * rotateSpeed * Time.deltaTime);

        Vector3 angle = new Vector3(
        horizontal * rotateSpeed,
        vertical * rotateSpeed,
        0
        );

        transform.eulerAngles += new Vector3(angle.y, angle.x);
    }
}
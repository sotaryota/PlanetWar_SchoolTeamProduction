using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMove_Solo : MonoBehaviour
{
    [Header("�J�����ƃI�[�f�B�I")]
    [SerializeField] Camera myCamera;
    [SerializeField] AudioSource audio;
    public AudioClip moveSE;
    [Header("�X�N���v�g")]
    public PlayerStatus_Solo playerStatus;
    [SerializeField] PlayerDataManager playerData;
    [SerializeField] PlayerAnimManeger playerAnimator;
    [SerializeField] PlayerGroundCheck ground;
    [SerializeField] PauseMenuSystem pause;
    [SerializeField] BattleToStory battleToStory;

    public Vector3 moveDirection;
    private float horizontal;
    private float vertical;
    private float waitcnt = 0.0f;

    CharacterController controller;
    Gamepad gamepad;
    Vector3 moveForward;

    private RaycastHit hit;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        pause = pause.GetComponent<PauseMenuSystem>();
    }

    private void Update()
    {
        //�|�[�Y���͏��������Ȃ�
        if (pause.PauseJudge()) { return; }

        //�v���C�������݂��Ȃ�or����ł���or��b���Ȃ珈�������Ȃ�
        if (playerStatus.GetState() == PlayerStatus_Solo.State.Non || playerStatus.GetState() == PlayerStatus_Solo.State.Dead ||
            playerStatus.GetState() == PlayerStatus_Solo.State.Talking)
        {
            playerAnimator.PlayAnimSetRun(false);
            return;
        }
        if (gamepad == null) { gamepad = Gamepad.current; }
        StickValue();
        PlayerLook();
        MoveOrStop();
    }

    //--------------------------------------
    //�J�����̌����𐳖ʂɂ��鏈��
    //--------------------------------------

    private void PlayerLook()
    {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(myCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        moveForward = cameraForward * vertical + myCamera.transform.right * horizontal;

        // �ړ������ɃX�s�[�h���|����
        moveDirection = moveForward * playerStatus.GetSpeed();

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    //--------------------------------------
    //�X�e�B�b�N�̒l�̎擾
    //--------------------------------------

    private void StickValue()
    {
        horizontal = gamepad.leftStick.x.ReadValue();
        vertical = gamepad.leftStick.y.ReadValue();
    }

    //--------------------------------------
    //�����̏���
    //--------------------------------------

    private void MoveOrStop()
    {
        //�X�e�B�b�N���͂���������
        if (horizontal <= 0.1 && horizontal >= -0.1 && vertical <= 0.1 && vertical >= -0.1)
        {
            //Catch��ԂłȂ��Ȃ�
            if (playerStatus.GetState() != PlayerStatus_Solo.State.Catch && playerStatus.GetState() != PlayerStatus_Solo.State.Jump)
            {
                //�v���C����Stay��Ԃ�
                playerStatus.SetState(PlayerStatus_Solo.State.Stay);
            }

            //�ҋ@���Ԃ̃J�E���g
            waitcnt += Time.deltaTime;

            //15�b��1�񏈗��{�C�X��炷
            if (waitcnt > 15.0f)
            {
                this.GetComponent<PlayerSEManager>().WaitVoice();

                //�ҋ@�{�C�X�̃J�E���g��0��
                waitcnt = 0.0f;

                playerAnimator.PlayAnimAppeal();
            }
            //�A�j���[�V����
            playerAnimator.PlayAnimSetRun(false);
        }
        else
        {
            //Catch��ԂłȂ��Ȃ�
            if (playerStatus.GetState() != PlayerStatus_Solo.State.Catch && playerStatus.GetState() != PlayerStatus_Solo.State.Jump)
            {
                //�v���C����Move��Ԃ�
                playerStatus.SetState(PlayerStatus_Solo.State.Move);
            }

            //�ړ�����
            controller.Move(moveDirection * Time.deltaTime);

            //�ҋ@�{�C�X�̃J�E���g��0��
            waitcnt = 0.0f;

            //�A�j���[�V����
            playerAnimator.PlayAnimSetRun(true);

        }
    }
    void RunSE()
    {
        audio.PlayOneShot(moveSE);
    }
}
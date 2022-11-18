using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ailian1_Move : MonoBehaviour
{
    [Header("�J����")]
    [SerializeField]
    Camera myCamera;
    [Header("�X�N���v�g")]
    public PlayerStatus playerStatus;
    [SerializeField]
    PlanetCatchRelease planetCatchRelease;
    Rigidbody rb;
   
    [SerializeField] PlayerAnimManeger playerAnimator;

    private Vector3 moveDirection;
  
    Vector3 moveForward;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //�v���C�������݂��Ȃ�or����ł���Ȃ珈�������Ȃ�
        if (playerStatus.GetState() == PlayerStatus.State.Non || playerStatus.GetState() == PlayerStatus.State.Dead)
        { return; }

   
        PlayerLook();
        MoveOrStop();

    }

    //--------------------------------------
    //�J�����̌����𐳖ʂɂ��鏈��
    //--------------------------------------

    private void PlayerLook()
    {
        

        // �ړ������ɃX�s�[�h���|����
        moveDirection = moveForward * playerStatus.GetSpeed();

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    //--------------------------------------
    //�����̏���
    //--------------------------------------
    private void MoveOrStop()
    {
        //�X�e�B�b�N���͂���������
        //if (horizontal <= 0.1 && horizontal >= -0.1 && vertical <= 0.1 && vertical >= -0.1)
        //{
        //    //Catch��ԂłȂ��Ȃ�
        //    if (playerStatus.GetState() != PlayerStatus.State.Catch)
        //    {
        //        //�v���C����Stay��Ԃ�
        //        playerStatus.SetState(PlayerStatus.State.Stay);
        //    }

        //    //��������
        //    rb.AddForce(-rb.velocity * (Time.deltaTime * 20));

        //    //�A�j���[�V����
        //    playerAnimator.PlayAnimSetRun(false);
        //}
        //else
        //{
        //    //Catch��ԂłȂ��Ȃ�
        //    if (playerStatus.GetState() != PlayerStatus.State.Catch)
        //    {
        //        //�v���C����Move��Ԃ�
        //        playerStatus.SetState(PlayerStatus.State.Move);
        //    }
        //    //�ړ�����
        //    rb.AddForce(moveDirection * Time.deltaTime - rb.velocity * (Time.deltaTime * 20));

        //    //�ړ��������X�s�[�h���グ��
        //    playerStatus.SpeedUp(moveDirection.magnitude * Time.deltaTime / 100);


        //    //�A�j���[�V����
        //    playerAnimator.PlayAnimSetRun(true);

        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob1_Move : MonoBehaviour
{
    [Header("�J����")]
    [Header("�X�N���v�g")]
    public Rob1_Status rob1Status;
    Rigidbody rb;
    [SerializeField] Rob1_AnimManeger rob1Animator;

    private Vector3 moveDirection;

    Vector3 moveForward;

    [SerializeField] GameObject player;

    [SerializeField] bool sensing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //�G�l�~�[�����݂��Ȃ�or����ł���Ȃ珈�������Ȃ�
        if (rob1Status.GetState() == Rob1_Status.State.Non || rob1Status.GetState() == Rob1_Status.State.Dead)
        { return; }

        PlayerLook();

        if (sensing == false)
        {
            MoveOrStop();
        }
    }

    //--------------------------------------
    //�J�����̌����𐳖ʂɂ��鏈��
    //--------------------------------------

    private void PlayerLook()
    {
        this.transform.LookAt(player.transform);

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        moveForward = Vector3.forward;

        // �ړ������ɃX�s�[�h���|����
        moveDirection = moveForward * rob1Status.GetSpeed();

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

            //Catch��ԂłȂ��Ȃ�
            if (rob1Status.GetState() != Rob1_Status.State.Catch)
            {
                //�v���C����Move��Ԃ�
                rob1Status.SetState(Rob1_Status.State.Move);
            }

            //�ړ�����
            rb.AddForce(moveDirection * Time.deltaTime - rb.velocity * (Time.deltaTime * 20));

            //�A�j���[�V����
            rob1Animator.PlayRob1AnimSetRun(true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other == player)
            sensing = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other == player)
            sensing = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetCatchRelease : MonoBehaviour
{
    Gamepad gamepad;
    
    [Header("�L���b�`����f���ƈʒu")]
    [SerializeField]
    private GameObject planet;
    [SerializeField]
    private GameObject planetPos;

    [Header("�X�e�[�^�X�Ǘ��X�N���v�g")]
    public PlayerStatus playerStatus;
    [SerializeField] PlayerAnimManeger playerAnimator;

    [Header("�f���𓊂������̃E�F�C�g")]
    [SerializeField]
    private float waitTime;
    public bool throwFlag = true;

        // Update is called once per frame
    void Update()
    {
        if (gamepad == null)
        {
            gamepad = Gamepad.all[playerStatus.GetID()];
        }

        if (planet)
        {
            //�������Ă���f���̈ʒu��r�̈ʒu��
            planet.transform.position = planetPos.transform.position;
            PlanetThrow();
            PlanetRelease();
        }
    }

    //--------------------------------------
    //�f���𓊂��鏈��
    //--------------------------------------

    private void PlanetThrow()
    {
        //�f�����������Ă��Ȃ��Ȃ珈�������Ȃ�
        if (!planet) return;

        //A�{�^���������ꂽ��
        if (gamepad.buttonEast.wasPressedThisFrame)
        {
            PlanetStateMachine stateMachine = planet.GetComponent<PlanetStateMachine>();
            PlanetThrowMove throwMove = planet.GetComponent<PlanetThrowMove>();

            //�f���̏�Ԃ�Throw�ɂ���
            stateMachine.SetState(PlanetStateMachine.State.Throw);

            //�f���̃X�s�[�h�Ɣ�ԕ��������߂�
            Vector3 throwSpeed = new Vector3(0, 0, 10);
            Vector3 playerAngle = this.transform.rotation.eulerAngles;
            Vector3 throwAngle = new Vector3(0, playerAngle.y, 0);
            throwMove.ThrowMoveSetting(throwSpeed, throwAngle);

            //������{�C�X�Đ�
            this.GetComponent<PlayerSEManager>().ThrowVoice();

            //�ϐ�����ɂ��Ĕ�΂�
            planet = null;

            //�͏㏸
            playerStatus.PowerUp(30);

            //�A�j���[�V����
            playerAnimator.PlayAnimThrow();

            //�d������
            StartCoroutine("ThrowWait");
        }
    }

    //--------------------------------------
    //���������̍d������
    //--------------------------------------

    IEnumerator ThrowWait()
    {
        throwFlag = false;

        yield return new WaitForSeconds(waitTime);

        //�v���C����Stay��Ԃɂ���
        playerStatus.SetState(PlayerStatus.State.Stay);

        throwFlag = true;
    }

    //--------------------------------------
    //�f�������������
    //--------------------------------------

    private void PlanetRelease()
    {
        //X�{�^���������ꂽ��
        if (gamepad.buttonNorth.isPressed)
        {
            //�f���̏�Ԃ�Idle��Ԃɖ߂�
            PlanetStateMachine stateMachine = planet.GetComponent<PlanetStateMachine>();
            stateMachine.SetState(PlanetStateMachine.State.Idle);
            //�v���C����Stay��Ԃɂ���
            playerStatus.SetState(PlayerStatus.State.Stay);
            //�ϐ�����ɂ���
            planet = null;
        }
    }

    //--------------------------------------
    //�f���Ƃ̔���Ƃ��ޏ���
    //--------------------------------------

    private void OnTriggerStay(Collider other)
    {
        //�d�����Ԓ��͏��������Ȃ�
        if (!throwFlag) return;

        //�f�����������Ă���ꍇ�͏��������Ȃ�
        if (planet) { return; }
        //�f���ł���ꍇ
        if (other.gameObject.tag == "Planet")
        {
            //�f���̏�Ԃ�Idle�Ȃ�
            if (other.GetComponent<PlanetStateMachine>().GetState() == PlanetStateMachine.State.Idle)
            {
                //�{�^���������ꂽ
                if (gamepad.buttonEast.isPressed)
                {
                    //�v���C���[�̃p���[+�E�F�C�g�����f���̏d����������
                    if (other.GetComponent<PlanetData>().GetWeight() <= playerStatus.GetPower() + (playerStatus.GetDefense()))   
                    {
                        //�v���C����Catch��Ԃ�
                        playerStatus.SetState(PlayerStatus.State.Catch);

                        //�f����ID�����g��ID�Ɠ����ɂ���
                        other.GetComponent<PlanetThrowHit>().catchPlayerID = playerStatus.GetID();

                        //�f��������
                        planet = other.gameObject;

                        //�v���l�b�g�̏�Ԃ�Catch��Ԃɂ���
                        planet.GetComponent<PlanetStateMachine>().SetState(PlanetStateMachine.State.Catch);
                    }
                }
            }
        }
    }
}

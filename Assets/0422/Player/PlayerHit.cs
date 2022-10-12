using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private Animator animator;
    PlayerStatus status;

    private void Start()
    {
        animator = GetComponent<Animator>();
        status   = GetComponent<PlayerStatus>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //�q�b�g�����̂��v���C���[�̏ꍇ�͖���
        if(other.transform.tag == "Player") { return; }

        //�q�b�g�����̂��q�b�g�G���A�̏ꍇ�͖���
        if (other.transform.tag == "CatchArea") { return; }

        //�v���l�b�g��Throw���
        if (other.GetComponent<PlanetStateMachine>().GetState() == PlanetStateMachine.State.Throw)
        {
            //�v���l�b�g��ID�����g�ƈ�v����ꍇ�͏��������Ȃ�
            if (other.GetComponent<PlanetThrowHit>().catchPlayerID == status.GetID()) { return; }

            //�v���C����Catch��Ԃ̎�
            if(status.GetState() == PlayerStatus.State.Catch)
            {
                //�_���[�W��ԂɕύX
                status.SetState(PlayerStatus.State.Damage);

                //Catch��Ԃɖ߂�
                status.SetState(PlayerStatus.State.Catch);
            }
            else
            {
                //Catch�łȂ��Ȃ�_���[�W��ԂɕύX���邾��
                status.SetState(PlayerStatus.State.Damage);
            }

            //�_���[�W����
            status.Damage(other.GetComponent<PlanetData>().GetDamage());

            //�f�B�t�F���X���㏸
            status.DefenseUp(25 + (other.GetComponent<PlanetData>().GetDamage() / 5));

            //�A�j���[�V����
            animator.SetTrigger("damage");

            //HP��0�ȉ��̎�
            if (status.GetHp() <= 0)
            {
                //�v���C�������S��ԂɕύX
                status.SetState(PlayerStatus.State.Dead);

                //�A�j���[�V����
                animator.SetTrigger("die");

                //���S�{�C�X�Đ�
                this.GetComponent<PlayerSEManager>().DeathVoice();
            }
            else
            {
                //�_���[�W�{�C�X�Đ�
                this.GetComponent<PlayerSEManager>().DamageVoice();
            }
        }
    }
}

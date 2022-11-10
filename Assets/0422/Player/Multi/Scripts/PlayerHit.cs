using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private Animator animator;
    PlayerStatus status;
    [SerializeField] PlayerDead dead;
    [SerializeField] PlayerEscape escape;

    [SerializeField] PlayerAnimManeger playerAnimator;
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

            
            //�_���[�W����
            status.Damage(other.GetComponent<PlanetData>().GetDamage());

            //�f�B�t�F���X���㏸
            status.DefenseUp(other.GetComponent<PlanetData>().GetDamage() + escape.GetCost());

            //HP��0�ȉ��̎�
            if (status.GetHp() <= 0)
            {
                //�v���C�������S��ԂɕύX
                status.SetState(PlayerStatus.State.Dead);

                dead.SetDeadState(PlayerDead.DeadState.die);
            }
            else
            {
                //�v���C����Catch��Ԃ̎�
                if (status.GetState() == PlayerStatus.State.Catch)
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

                Debug.Log("�f���q�b�g");

                //�_���[�W�{�C�X�Đ�
                this.GetComponent<PlayerSEManager>().DamageVoice();

                //�A�j���[�V����
                playerAnimator.PlayAnimDamage();
            }
        }
    }

    private void Update()
    {
        if (dead.GetDead() == PlayerDead.DeadState.non)
        {
            //HP��0�ȉ��̎�
            if (status.GetHp() <= 0)
            {
                //�v���C�������S��ԂɕύX
                status.SetState(PlayerStatus.State.Dead);

                dead.SetDeadState(PlayerDead.DeadState.exhausted);
            }
        }
    }
}
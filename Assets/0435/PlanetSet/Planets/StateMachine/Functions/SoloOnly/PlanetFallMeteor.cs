using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFallMeteor : PlanetStateFanction
{
    private Vector3 startPos;//�����ʒu�iLerp�Ŏg�p�j

    [Header("�ڒn���G�t�F�N�g")]
    [SerializeField]
    private GameObject crashEffect;

    [Header("�������x")]
    [SerializeField]
    private float moveTime;
    private float startTime;

    [Header("�^�[�Q�b�g���W")]
    public Vector3 targetPos;//�^�[�Q�b�g���W

    private bool moveFlag;//�ړ����邩�𔻒�i�u Y�� <= �^�[�Q�b�g���WY �v�Ŗ������j
    public bool GetMoveFlag() { return moveFlag; }

    private void Start()
    {
        //�����ʒu�ۑ�
        startPos = transform.position;

        //�ړ��J�n
        moveFlag = true;

        //�J�n���Ԃ𓾂�
        startTime = Time.time;
    }

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        if (moveFlag)
        {
            //Y�����O�܂ňړ��i���Ԋ����j
            float distance = Vector3.Distance(startPos, targetPos);
            float moveVal = (Time.time - startTime) / distance;
            transform.position = Vector3.Lerp(startPos, targetPos, moveVal * moveTime);

            //���W���O�t�߂ɂȂ�����
            if (transform.position.y <= targetPos.y)
            {
                //Y���W���w��̍��W�֏㏑��
                transform.position = new Vector3(transform.position.x, targetPos.y, transform.position.z);

                //�G�t�F�N�g����
                GameObject effect = Instantiate(crashEffect);
                effect.transform.position = this.transform.position;

                //�ړ��I��
                moveFlag = false;
            }

        }

        return PlanetStateMachine.State.Now;
    }
}

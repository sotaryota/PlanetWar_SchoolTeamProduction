using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAimAssist : PlanetStateFanction
{
    [Header("�G�C���A�V�X�g�F�␳����G�i�����Q�Ɓj"), SerializeField]
    private GameObject targetEnemy;
    private bool assistFlag = true;

    [Header("�J������}��"), SerializeField]
    private Camera targetCamera;

    [Header("��ʂ̒��S����A�V�X�g���s���͈�"), Range(0,1), SerializeField]
    private float assistScreenArea;

    [Header("�⊮�ő勗��"), SerializeField]
    private float assistDistance;

    [Header("�⊮�l"), SerializeField]
    private float rotateValue;

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        if (assistFlag)
        {
            //�P�x����������s��Ȃ�
            assistFlag = false;

            SearchEnemy();

            if (targetEnemy)
            {
                Assist();
            }
        }

        return PlanetStateMachine.State.Now;
    }

    private void SearchEnemy()
    {
        //�G�^�O���t���Ă���I�u�W�F�N�g�̌������s��
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        //����p�J������L����
        targetCamera.enabled = true;

        //���������G�̐�����������s��
        foreach (GameObject en in enemys)
        {
            //�G�̍��W�����߂�
            var enemyScreenPos = targetCamera.WorldToViewportPoint(en.transform.position);

            //��ʒ��S����̃A�V�X�g�G���A���쐬
            float middle = 0.5f - (assistScreenArea / 2);
            float size = assistScreenArea;
            Rect area = new Rect(middle, middle, size, size);

            //�A�V�X�g�G���A�������Z�o����
            if (area.Contains(enemyScreenPos))
            {
                if (Vector3.Dot((this.transform.position - en.transform.position).normalized, transform.forward) <= 0) { 
                    //�Q�Ƃ�����ꍇ�͔��肵�ۑ��A�Ȃ��ꍇ�͂��̂܂ܕۑ�
                    if (targetEnemy)
                    {
                        //�G���o�^����Ă���G���߂������ꍇ�͓���ւ�
                        float nowTargetPos = (targetEnemy.transform.position - this.transform.position).magnitude;
                        float newTargetPos = (en.transform.position - this.transform.position).magnitude;
                        if (newTargetPos < nowTargetPos)
                        {
                            targetEnemy = en;
                        }
                    }
                    else
                    {
                        //�Q�Ƃ������̂ŕۑ�
                        targetEnemy = en;
                    }
            }
            }          
        }

        //�e�ʍ팸�̈׃J�����폜
        //Destroy(targetCamera);
    }

    public void Assist()
    {
        //�ق�̏��������G�̕����֌X������
        float targetDis = (targetEnemy.transform.position - this.transform.position).magnitude;
        if(targetDis <= assistDistance)
        {
            Vector3 targetPos = targetEnemy.transform.position;
            targetPos.y = this.transform.position.y;
            this.transform.LookAt(targetPos);
        }
    }
}

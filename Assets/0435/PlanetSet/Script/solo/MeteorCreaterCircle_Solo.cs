using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCreaterCircle_Solo : MonoBehaviour
{
    [Header("�쐬����覐�")]
    [SerializeField]
    private GameObject[] createPlanets = new GameObject[1];

    [Header("覐΂̐����ʒu�𗐐��Ŏ擾")]
    [SerializeField]
    private Vector3 min_firstCreatePos;
    [SerializeField]
    private Vector3 max_firstCreatePos;
    [Header("�A�^�b�`����Ă���I�u�W�F�N�g�̈ʒu�ֈړ���Gizumos�\���ؑ�")]
    [SerializeField]
    private bool addMyPos = false;
    [SerializeField]
    private bool gizumos_CreateArea = true;

    [Header("�쐬����覐΂��������G���A�𗐐��Ŏw��")]
    [SerializeField]
    Vector3 min_TargetPos;
    [SerializeField]
    Vector3 max_TargetPos;

    [Header("���S����̋���")]
    [SerializeField]
    float targetDistance;
    [SerializeField]
    float notTargetDistance;
    [Header("�쐬����覐΁i�v�f�����ő吶�����j")]
    [SerializeField]
    private GameObject[] meteors = new GameObject[10];

    [Header("覐ΐ������ɗ^������")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private AudioSource source;

    // Update is called once per frame
    void Update()
    {
        Vector3 targeter = this.transform.position;
        //targeter.y = player.transform.position.y;
        if ((targeter - player.transform.position).magnitude < 200)
        {
            for (int i = 0; i < meteors.Length; ++i)
            {
                //覐΂��i�[����Ă��Ȃ������ꍇ�A�f���𐶐�
                if (!meteors[i])
                {
                    //�ʒu������
                    GameObject go = Instantiate(createPlanets[Random.Range(0, createPlanets.Length)]);

                    //�������W�̓����_��
                    Vector3 createPos = new Vector3(
                        Random.Range(min_firstCreatePos.x, max_firstCreatePos.x),
                        Random.Range(min_firstCreatePos.y, max_firstCreatePos.y),
                        Random.Range(min_firstCreatePos.z, max_firstCreatePos.z)
                        );

                    if (addMyPos)
                    {
                        createPos += this.transform.position;
                    }

                    //�I�[�f�B�I�\�[�X�����蓖�Ă�
                    PlanetAttackHit pah;
                    if (pah = go.GetComponent<PlanetAttackHit>())
                    {
                        pah.source = this.source;
                    }

                    //�v���C���̈ʒu�����蓖�Ă�
                    PlanetStateMachine psm;
                    if (psm = go.GetComponent<PlanetStateMachine>())
                    {
                        psm.middleObject = player;
                    }

                    go.transform.position = createPos;

                    //�ڕW���W�ݒ�
                    bool targetSetLoop = true;
                    Vector3 targetPos = Vector3.zero;
                    while (targetSetLoop == true)
                    {
                        targetPos = new Vector3(
                            Random.Range(min_TargetPos.x, max_TargetPos.x),
                            Random.Range(min_TargetPos.y, max_TargetPos.y),
                            Random.Range(min_TargetPos.z, max_TargetPos.z));

                        if (addMyPos)
                        {
                            targetPos += this.transform.position;
                        }

                        float distance = (targetPos - this.transform.position).magnitude;
                        if (distance <= targetDistance)
                        {
                            if(distance >= notTargetDistance)
                            {
                                targetSetLoop = false;
                            }
                        }
                    }

                    //�K�p
                    PlanetFallMeteor pfm;
                    if (pfm = go.GetComponent<PlanetFallMeteor>())
                    {
                        pfm.targetPos = targetPos;
                    }

                    //覐΂�R�Â�
                    go.transform.parent = this.transform;
                    meteors[i] = go;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (gizumos_CreateArea)
        {
            Gizmos.color = Color.blue;

            Vector3 targetSize = new Vector3(
                max_TargetPos.x - min_TargetPos.x,
                max_TargetPos.y - min_TargetPos.y,
                max_TargetPos.z - min_TargetPos.z
            );

            Vector3 targetPos = min_TargetPos + targetSize / 2;

            if (addMyPos)
            {
                targetPos += this.transform.position;
            }

            Gizmos.DrawCube(targetPos, targetSize);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetPos, notTargetDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(targetPos, targetDistance);
        }
    }
}
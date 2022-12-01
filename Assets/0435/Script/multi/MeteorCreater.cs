using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCreater : MonoBehaviour
{
    [Header("�쐬����覐�")]
    [SerializeField]
    private GameObject[] createPlanets = new GameObject[1];

    [Header("覐΂̐����ʒu(X = min, Y = max")]
    [SerializeField]
    private Vector2 firstCreatePos;

    [Header("�쐬����覐΂��������G���A�𗐐��Ŏw��")]
    [SerializeField]
    Vector3 minTargetPos;
    [SerializeField]
    Vector3 maxTargetPos;

    [Header("�쐬����覐΁i�v�f�����ő吶�����j")]
    [SerializeField]
    private GameObject[] meteors = new GameObject[10];

    [Header("覐ΐ������ɗ^������")]
    [SerializeField]
    private AudioSource source;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < meteors.Length; ++i)
        {
            //覐΂��i�[����Ă��Ȃ������ꍇ�A�f���𐶐�
            if (!meteors[i])
            {
                //�ʒu������
                GameObject go = Instantiate(createPlanets[Random.Range(0, createPlanets.Length)]);

                //���W�̓����_��
                float cPosX = 0;
                float cPosZ = 0;
                if(Random.Range(0,2) == 0)
                {
                    //X�������R
                    cPosX = Random.Range(-firstCreatePos.y, firstCreatePos.y);
                    cPosZ = Random.Range(firstCreatePos.x, firstCreatePos.y);
                }
                else
                {
                    //Z�������R
                    cPosX = Random.Range(firstCreatePos.x, firstCreatePos.y);
                    cPosZ = Random.Range(-firstCreatePos.y, firstCreatePos.y);
                }
                
                Vector3 createPos = new Vector3(cPosX,0,cPosZ);
                //�I�[�f�B�I�\�[�X�����蓖�Ă�
                PlanetAttackHit pah;
                if(pah = go.GetComponent<PlanetAttackHit>())
                {
                    pah.source = this.source;
                }

                //X���}�C�i�X����
                if (Random.Range(0, 2) == 1)
                {
                    createPos = new Vector3(
                        createPos.x * -1,
                        createPos.y,
                        createPos.z
                        );
                }
                //Z���}�C�i�X����
                if (Random.Range(0, 2) == 1)
                {
                    createPos = new Vector3(
                        createPos.x,
                        createPos.y,
                        createPos.z * -1
                        );
                }

                go.transform.position = createPos;

                //�p�x����          
                Vector3 targetPos = new Vector3(
                    Random.Range(minTargetPos.x, maxTargetPos.x),
                    Random.Range(minTargetPos.y, maxTargetPos.y),
                    Random.Range(minTargetPos.z, maxTargetPos.z));
                go.transform.LookAt(targetPos, Vector3.up);

                //覐΂�R�Â�
                meteors[i] = go;
            }
        }
    }
}
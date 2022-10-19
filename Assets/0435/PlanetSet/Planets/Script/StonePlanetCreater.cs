using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePlanetCreater : MonoBehaviour
{
    [Header("�쐬����f��")]
    [SerializeField]
    private GameObject[] createPlanets = new GameObject[1];

    [Header("�f���̐����ʒu")]
    [SerializeField]
    private Vector3 firstPos_Min;
    [SerializeField]
    private Vector3 firstPos_Max;

    [Header("�쐬�����f���i�v�f�����ő吶�����j")]
    [SerializeField]
    private GameObject[] meteors = new GameObject[10];

    [Header("�f���������ɗ^������")]
    [SerializeField]
    private AudioSource source;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < meteors.Length; ++i)
        {
            //覐΂��i�[����Ă��Ȃ������ꍇ�A�f���𐶐�
            if (!meteors[i])
            {
                //�ʒu������
                GameObject go = Instantiate(createPlanets[Random.Range(0, createPlanets.Length)]);

                //���W�̓����_��
                Vector3 cPos = Vector3.zero;
                if (Random.Range(0, 2) == 0)
                {
                    //X�������R
                    cPos.x = Random.Range(-firstPos_Max.y, firstPos_Max.y);
                    cPos.y = Random.Range(firstPos_Min.x, firstPos_Max.y);
                    cPos.z = Random.Range(firstPos_Min.x, firstPos_Max.y);
                }
                else
                {
                    //Z�������R
                    cPos.x = Random.Range(firstPos_Min.y, firstPos_Max.y);
                    cPos.y = Random.Range(firstPos_Min.x, firstPos_Max.y);
                    cPos.z = Random.Range(-firstPos_Max.x, firstPos_Max.y);
                }
                Vector3 createPos = cPos;

                //�I�[�f�B�I�\�[�X�����蓖�Ă�
                PlanetAttackHit pah;
                if (pah = go.GetComponent<PlanetAttackHit>())
                {
                    pah.source = this.source;
                }

                //X���}�C�i�X����
                if (Random.Range(0, 2) == 1)
                {
                    createPos = new Vector3(
                        -createPos.x,
                        createPos.y,
                        createPos.z
                        );
                }

                //Y���}�C�i�X����
                if (Random.Range(0, 2) == 0)
                {
                    createPos = new Vector3(
                        createPos.x,
                        -createPos.y,
                        createPos.z
                        );
                }

                //Z���}�C�i�X����
                if (Random.Range(0, 2) == 1)
                {
                    createPos = new Vector3(
                        createPos.x,
                        createPos.y,
                        -createPos.z
                        );
                }

                go.transform.position = createPos;

                //�f����R�Â�
                meteors[i] = go;
            }
        }
    }
}

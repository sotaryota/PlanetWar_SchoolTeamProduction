using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToCreate : MonoBehaviour
{
    [Header("�쐬���������́i���W�̒������͈�؍s��Ȃ��j")]
    [SerializeField]
    private GameObject[] objects = new GameObject[1];

    [Header("�^�C�}�[�X�N���v�g�Ɛ������鎞��")]
    [SerializeField]
    private Timer timerScript;
    [SerializeField]
    private float createTime;
    //private float nowTime;

    [Header("�f���������ɗ^������")]
    [SerializeField]
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        //nowTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //nowTime += Time.deltaTime;
        //if(nowTime >= createTime)
        if (timerScript.GetNowTime() <= createTime)
        {
            for(int i = 0; i < objects.Length; ++i)
            {
                if (objects[i])
                {
                    GameObject go = Instantiate(objects[i]);
                    if (go.GetComponent<PlanetAttackHit>())
                    {
                        go.GetComponent<PlanetAttackHit>().source = source;
                    }
                
                }
            }
            Destroy(this);
        }
    }
}

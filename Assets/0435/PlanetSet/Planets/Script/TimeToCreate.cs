using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToCreate : MonoBehaviour
{
    [Header("作成したいもの（座標の調整等は一切行わない）")]
    [SerializeField]
    private GameObject[] objects = new GameObject[1];

    [Header("タイマースクリプトと生成する時間")]
    [SerializeField]
    private Timer timerScript;
    [SerializeField]
    private float createTime;
    //private float nowTime;

    [Header("惑星生成時に与える情報")]
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

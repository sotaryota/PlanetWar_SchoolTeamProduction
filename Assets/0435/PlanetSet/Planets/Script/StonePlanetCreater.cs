using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePlanetCreater : MonoBehaviour
{
    [Header("作成する惑星")]
    [SerializeField]
    private GameObject[] createPlanets = new GameObject[1];

    [Header("惑星の生成位置")]
    [SerializeField]
    private Vector3 firstPos_Min;
    [SerializeField]
    private Vector3 firstPos_Max;

    [Header("作成した惑星（要素数が最大生成数）")]
    [SerializeField]
    private GameObject[] meteors = new GameObject[10];

    [Header("惑星生成時に与える情報")]
    [SerializeField]
    private AudioSource source;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < meteors.Length; ++i)
        {
            //隕石が格納されていなかった場合、惑星を生成
            if (!meteors[i])
            {
                //位置を決定
                GameObject go = Instantiate(createPlanets[Random.Range(0, createPlanets.Length)]);

                //座標はランダム
                Vector3 cPos = Vector3.zero;
                if (Random.Range(0, 2) == 0)
                {
                    //X軸が自由
                    cPos.x = Random.Range(-firstPos_Max.y, firstPos_Max.y);
                    cPos.y = Random.Range(firstPos_Min.x, firstPos_Max.y);
                    cPos.z = Random.Range(firstPos_Min.x, firstPos_Max.y);
                }
                else
                {
                    //Z軸が自由
                    cPos.x = Random.Range(firstPos_Min.y, firstPos_Max.y);
                    cPos.y = Random.Range(firstPos_Min.x, firstPos_Max.y);
                    cPos.z = Random.Range(-firstPos_Max.x, firstPos_Max.y);
                }
                Vector3 createPos = cPos;

                //オーディオソースを割り当てる
                PlanetAttackHit pah;
                if (pah = go.GetComponent<PlanetAttackHit>())
                {
                    pah.source = this.source;
                }

                //X軸マイナス判定
                if (Random.Range(0, 2) == 1)
                {
                    createPos = new Vector3(
                        -createPos.x,
                        createPos.y,
                        createPos.z
                        );
                }

                //Y軸マイナス判定
                if (Random.Range(0, 2) == 0)
                {
                    createPos = new Vector3(
                        createPos.x,
                        -createPos.y,
                        createPos.z
                        );
                }

                //Z軸マイナス判定
                if (Random.Range(0, 2) == 1)
                {
                    createPos = new Vector3(
                        createPos.x,
                        createPos.y,
                        -createPos.z
                        );
                }

                go.transform.position = createPos;

                //惑星を紐づけ
                meteors[i] = go;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCreater : MonoBehaviour
{
    [Header("作成する惑星")]
    [SerializeField]
    private GameObject[] createPlanets = new GameObject[1];

    [Header("隕石の生成位置(X = min, Y = max")]
    [SerializeField]
    private Vector2 firstCreatePos;

    [Header("作成した隕石が向かうエリアを乱数で指定")]
    [SerializeField]
    Vector3 minTargetPos;
    [SerializeField]
    Vector3 maxTargetPos;

    [Header("作成した惑星（要素数が最大生成数）")]
    [SerializeField]
    private GameObject[] meteors = new GameObject[10]; 

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < meteors.Length; ++i)
        {
            //隕石が格納されていたら無効化
            if (!meteors[i])
            {
                //位置を決定
                GameObject go = Instantiate(createPlanets[Random.Range(0, createPlanets.Length)]);
                Vector3 createPos = new Vector3(
                    Random.Range(firstCreatePos.x, firstCreatePos.y),
                    0,
                    Random.Range(firstCreatePos.x, firstCreatePos.y));

                //X軸マイナス判定
                if (Random.Range(0, 2) == 1)
                {
                    createPos = new Vector3(
                        createPos.x * -1,
                        createPos.y,
                        createPos.z
                        );
                }
                //Z軸マイナス判定
                if (Random.Range(0, 2) == 1)
                {
                    createPos = new Vector3(
                        createPos.x,
                        createPos.y,
                        createPos.z * -1
                        );
                }

                go.transform.position = createPos;

                //角度決定          
                Vector3 targetPos = new Vector3(
                    Random.Range(minTargetPos.x, maxTargetPos.x),
                    Random.Range(minTargetPos.y, maxTargetPos.y),
                    Random.Range(minTargetPos.z, maxTargetPos.z));
                go.transform.LookAt(targetPos, Vector3.up);

                //隕石を紐づけ
                meteors[i] = go;
            }
        }
    }
}

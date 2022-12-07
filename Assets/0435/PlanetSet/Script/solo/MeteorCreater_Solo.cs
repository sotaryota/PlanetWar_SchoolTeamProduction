using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCreater_Solo : MonoBehaviour
{
    [Header("作成する隕石")]
    [SerializeField]
    private GameObject[] createPlanets = new GameObject[1];

    [Header("隕石の生成位置を乱数で取得")]
    [SerializeField]
    private Vector3 min_firstCreatePos;
    [SerializeField]
    private Vector3 max_firstCreatePos;
    [Header("アタッチされているオブジェクトの位置へ移動＆Gizumos表示切替")]
    [SerializeField]
    private bool addMyPos = false;
    [SerializeField]
    private bool gizumos_CreateArea = true;

    [Header("作成した隕石が向かうエリアを乱数で指定")]
    [SerializeField]
    Vector3 min_TargetPos;
    [SerializeField]
    Vector3 max_TargetPos;

    [Header("作成した隕石（要素数が最大生成数）")]
    [SerializeField]
    private GameObject[] meteors = new GameObject[10];

    [Header("隕石生成時に与える情報")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private AudioSource source;

    // Update is called once per frame
    void Update()
    {
        Vector3 targeter = this.transform.position;
        //targeter.y = player.transform.position.y;
        if ((targeter - player.transform.position).magnitude < 65)
        {
            for (int i = 0; i < meteors.Length; ++i)
            {
                //隕石が格納されていなかった場合、惑星を生成
                if (!meteors[i])
                {
                    //位置を決定
                    GameObject go = Instantiate(createPlanets[Random.Range(0, createPlanets.Length)]);

                    //生成座標はランダム
                    Vector3 createPos = new Vector3(
                        Random.Range(min_firstCreatePos.x, max_firstCreatePos.x),
                        Random.Range(min_firstCreatePos.y, max_firstCreatePos.y),
                        Random.Range(min_firstCreatePos.z, max_firstCreatePos.z)
                        );

                    if (addMyPos)
                    {
                        createPos += this.transform.position;
                    }

                    //オーディオソースを割り当てる
                    PlanetAttackHit pah;
                    if (pah = go.GetComponent<PlanetAttackHit>())
                    {
                        pah.source = this.source;
                    }

                    //プレイヤの位置を割り当てる
                    PlanetStateMachine psm;
                    if (psm = go.GetComponent<PlanetStateMachine>())
                    {
                        psm.middleObject = player;
                    }

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

                    //目標座標設定          
                    Vector3 targetPos = new Vector3(
                        Random.Range(min_TargetPos.x, max_TargetPos.x),
                        Random.Range(min_TargetPos.y, max_TargetPos.y),
                        Random.Range(min_TargetPos.z, max_TargetPos.z));
                    if (addMyPos)
                    {
                        targetPos += this.transform.position;
                    }

                    //適用
                    PlanetFallMeteor pfm;
                    if (pfm = go.GetComponent<PlanetFallMeteor>())
                    {
                        pfm.targetPos = targetPos;
                    }

                    //隕石を紐づけ
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
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater_Start : MonoBehaviour
{
    [Header("EnemyCreate_Dataを挿入"), SerializeField]
    private EnemyCreater_Data ecd;

    [Header("生成する敵の名前（EnemyCreater_Data.EnemyName）"), SerializeField]
    private EnemyCreater_Data.EnemyName enemyName = EnemyCreater_Data.EnemyName.Error;

    //作成した敵Prefab
    public GameObject createPrefab;

    // Start is called before the first frame update
    void Start()
    {
        enemyName = GameObject.Find("DataManager").GetComponent<NPCDataManager>().enemyName;
        //敵を生成し、位置を指定位置に更新
        createPrefab = Instantiate(ecd.enemyData[(int)enemyName].prefab);
        createPrefab.transform.position = ecd.enemyData[(int)enemyName].pos;

        //このスクリプトを無効化
        this.enabled = false;
    }
}

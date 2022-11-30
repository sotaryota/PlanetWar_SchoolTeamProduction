using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater_Start : MonoBehaviour
{
    [Header("EnemyCreate_Dataを挿入"), SerializeField]
    private EnemyCreater_Data ecd;

    [Header("生成する敵の名前（EnemyCreater_Data.EnemyName）"), SerializeField]
    private EnemyCreater_Data.EnemyName enemyName = EnemyCreater_Data.EnemyName.Error;

    // Start is called before the first frame update
    void Start()
    {
        //敵を生成し、位置を指定位置に更新
        GameObject enemys = Instantiate(ecd.enemyData[(int)enemyName].prefab);
        enemys.transform.position = ecd.enemyData[(int)enemyName].pos;

        //このスクリプトを有効化
        this.enabled = false;
    }
}

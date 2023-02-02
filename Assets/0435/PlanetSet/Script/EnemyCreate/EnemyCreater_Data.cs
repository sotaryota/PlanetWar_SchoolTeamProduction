using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater_Data : MonoBehaviour
{
    public enum EnemyName
    {
        set1_Rob1_2_Turret_2,
        set2_Rob1_2_Rob2_1,
        set3_Rob2_2,
        set4_Rob1_1_Rob2_1_Turret_1,
        set5_Rob1_2_BoxRob_1,
        set6_BoxRob_1_Turret_2,
        set7_Turret_4,
        set8_Turret_2,
        set9_BoxRob_2,
        set10_Rob1_2_Rob2_1_Tuuret_1,
        set11_BoxRob_2_Rob_1_Rob2_1_Turret_2,

        set12_Fighter_1,

        //これより上に敵を追加すること
        ENUM_END,
        Error
    }

    [System.Serializable]
    public class Enemys
    {
        [HideInInspector]
        public GameObject prefab;
        [HideInInspector]
        public Vector3 pos;
    }

    [HideInInspector]
    public Enemys[] enemyData = new Enemys[1];

    public void CreateEnemy(EnemyName enum_EnemyName)
    {
        GameObject create = Instantiate(enemyData[(int)enum_EnemyName].prefab);
        create.transform.position = enemyData[(int)enum_EnemyName].pos;
    }
}

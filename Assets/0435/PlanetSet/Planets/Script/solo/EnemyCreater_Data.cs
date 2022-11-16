using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater_Data : MonoBehaviour
{
    public enum EnemyName
    {
        test,
        sab,
        a,
        b,
        c,
        d,



        //Ç±ÇÍÇÊÇËè„Ç…ìGÇí«â¡Ç∑ÇÈÇ±Ç∆
        ENUM_END
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

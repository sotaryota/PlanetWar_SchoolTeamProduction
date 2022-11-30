using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreater_Start : MonoBehaviour
{
    [Header("EnemyCreate_Data��}��"), SerializeField]
    private EnemyCreater_Data ecd;

    [Header("��������G�̖��O�iEnemyCreater_Data.EnemyName�j"), SerializeField]
    private EnemyCreater_Data.EnemyName enemyName = EnemyCreater_Data.EnemyName.Error;

    // Start is called before the first frame update
    void Start()
    {
        //�G�𐶐����A�ʒu���w��ʒu�ɍX�V
        GameObject enemys = Instantiate(ecd.enemyData[(int)enemyName].prefab);
        enemys.transform.position = ecd.enemyData[(int)enemyName].pos;

        //���̃X�N���v�g��L����
        this.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear_Solo : MonoBehaviour
{
    [SerializeField]
    EnemyCreater_Start enemyCreater_Start;

    [SerializeField]
    GameObject finishText;
    
    //オーディオ追加
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip winJingle;

    // Start is called before the first frame update
    void Start()
    {
        finishText.SetActive(false);
        audioSource.PlayOneShot(winJingle);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCreater_Start.createPrefab) { return; }

        finishText.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBreathController : MonoBehaviour
{
    [Header("滞空する時間"), SerializeField]
    private float stayTime;

    [Header("目標座標と弾丸速度"), SerializeField]
    private Vector3 targetPos;//終了座標
    private Vector3 startPos;//開始座標
    public void SetTarget(Vector3 pos)
    {
        targetPos = pos;
    }
    [SerializeField] private float speed;//補完値
    float nowCountPos;

    [Header("着弾時に生成するPrefab"), SerializeField]
    private GameObject prefab;

    private void Start()
    {
        startPos = this.transform.position;
        nowCountPos = 0;
    }

    private void Update()
    {
        stayTime -= Time.deltaTime;
        if(stayTime <= 0)
        {
            nowCountPos += speed * Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPos, targetPos, nowCountPos);
            if(nowCountPos >= 1)
            {
                GameObject go = Instantiate(prefab);
                go.transform.position = this.transform.position;
                Destroy(gameObject);
            }
        }
    }
}

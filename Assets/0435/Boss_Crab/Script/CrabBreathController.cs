using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBreathController : MonoBehaviour
{
    [Header("滞空する時間"), SerializeField]
    private float stayTime;
    private Rigidbody rigidbody;

    [Header("着弾時に生成するPrefab"), SerializeField]
    private GameObject prefab;

    private void Start()
    {
        if(rigidbody = this.GetComponent<Rigidbody>())
        {
            rigidbody.useGravity = false;
        }
    }

    private void Update()
    {
        stayTime -= Time.deltaTime;
        if(stayTime <= 0)
        {
            if (rigidbody)
            {
                rigidbody.useGravity = true;
            }
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Ground")
        {
            GameObject go = Instantiate(prefab);
            go.transform.position = this.transform.position;
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBreathController : MonoBehaviour
{
    [Header("‘Ø‹ó‚·‚éŠÔ"), SerializeField]
    private float stayTime;
    private Rigidbody rigidbody;

    [Header("’…’e‚É¶¬‚·‚éPrefab"), SerializeField]
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

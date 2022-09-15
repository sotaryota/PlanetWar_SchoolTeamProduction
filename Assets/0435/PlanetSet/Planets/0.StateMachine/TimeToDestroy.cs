using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToDestroy : MonoBehaviour
{
    [Header("”j‰ó‚Ü‚Å‚ÌŽžŠÔ")]
    [SerializeField]
    private float destroyTime;
    private float nowCount;

    // Start is called before the first frame update
    void Start()
    {
        nowCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        nowCount += Time.deltaTime;
        if (nowCount >= destroyTime)
        {
            Destroy(gameObject);
        }
    }
}

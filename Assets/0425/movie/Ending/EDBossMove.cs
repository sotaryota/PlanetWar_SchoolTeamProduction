using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDBossMove : MonoBehaviour
{
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private float dieFallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //死亡アニメーション
        bossAnimator.SetTrigger("Die");
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //下に沈む
        this.transform.Translate(new Vector3(0, -dieFallSpeed * Time.deltaTime, 0));
    }
}

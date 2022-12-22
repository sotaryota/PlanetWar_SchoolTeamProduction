using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAttackHit : MonoBehaviour
{
    [Header("UŒ‚‚Ìƒ_ƒ[ƒW"), SerializeField]
    private float damage;

    [Header("UŒ‚”ÍˆÍ—LŒøŽžŠÔ"), SerializeField]
    private float attackTime;
    private float nowCount;

    [Header("Ú×Ý’è")]
    [SerializeField] private bool hitOnce = true;
    [SerializeField] private bool timeNotUse = false;
    [SerializeField] private bool useTriggerCollision;
    [SerializeField] private bool useParticleCollision;
    private bool hitFlag;

    // Start is called before the first frame update
    void Start()
    {
        nowCount = 0;
        hitFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeNotUse)
        {
            nowCount += Time.deltaTime;
            if (nowCount >= attackTime)
            {
                hitFlag = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hitFlag == false) return;
        if (useTriggerCollision == false) return;

        PlayerStatus_Solo player;
        if (player = other.GetComponent<PlayerStatus_Solo>())
        {
            player.Damage(damage);
            if (hitOnce)
            {
                hitFlag = false;
            }   
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (hitFlag == false) return;
        if (useParticleCollision == false) return;

        PlayerStatus_Solo player;
        if (player = other.GetComponent<PlayerStatus_Solo>())
        {
            player.Damage(damage);
            if (hitOnce)
            {
                hitFlag = false;
            }
        }
    }
}

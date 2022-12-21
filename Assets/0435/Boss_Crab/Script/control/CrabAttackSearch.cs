using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAttackSearch : MonoBehaviour
{
    [Header("ÉTÅ[É`Ç∑ÇÈÇ©Ç«Ç§Ç©"), SerializeField]
    private bool searching = true;

    [Header("HitCheck"), SerializeField]
    private bool hit = false;

    public bool HitCheck()
    {
        return hit && searching;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player") return;
        hit = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != "Player") return;
        hit = false;
    }
}

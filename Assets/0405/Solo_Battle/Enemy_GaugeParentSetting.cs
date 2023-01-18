using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_GaugeParentSetting : MonoBehaviour
{
    void Start()
    {
        Canvas canvas = GameObject.Find("EnemyHPCanvas").GetComponent<Canvas>();
        this.transform.parent = canvas.transform;
        Destroy(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour
{
    [Header("チェックが入っていた場合、サイズをランダムで設定")]
    [SerializeField]
    private bool autoScaleSettingMode;
    [SerializeField]
    private Vector2 scaleRange;

    [Header("チェックが入っていた場合、サイズに比例して情報を自動で設定")]
    [SerializeField]
    private bool autoDataSettingMode;
    [SerializeField]
    private float settingRate_Damage;
    [SerializeField]
    private float settingRate_Weight;

    [Header("以下、手動で設定する場合のみ入力（上記チェックボックスなし）")]
    [SerializeField]
    private float damage = 0;
    public float GetDamage() { return damage; }
    [SerializeField]
    private float weight = 0;
    public float GetWeight() { return weight; }

    private void Awake()
    {
        if (autoScaleSettingMode)
        {
            float scaleNum = Random.Range(scaleRange.x, scaleRange.y);
            transform.localScale = new Vector3(scaleNum, scaleNum, scaleNum);
        }

        if (autoDataSettingMode)
        {
            damage = this.transform.localScale.magnitude * settingRate_Damage;
            weight = this.transform.localScale.magnitude * settingRate_Weight;
        }
    }

}

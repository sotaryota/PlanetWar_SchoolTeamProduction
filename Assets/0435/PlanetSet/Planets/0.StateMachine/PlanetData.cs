using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour
{
    [Header("�`�F�b�N�������Ă����ꍇ�A�T�C�Y�������_���Őݒ�")]
    [SerializeField]
    private bool autoScaleSettingMode;
    [SerializeField]
    private Vector2 scaleRange;

    [Header("�`�F�b�N�������Ă����ꍇ�A�T�C�Y�ɔ�Ⴕ�ď��������Őݒ�")]
    [SerializeField]
    private bool autoDataSettingMode;
    [SerializeField]
    private float settingRate_Damage;
    [SerializeField]
    private float settingRate_Weight;

    [Header("�ȉ��A�蓮�Őݒ肷��ꍇ�̂ݓ��́i��L�`�F�b�N�{�b�N�X�Ȃ��j")]
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

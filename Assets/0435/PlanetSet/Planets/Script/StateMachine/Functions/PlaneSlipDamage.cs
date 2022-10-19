using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSlipDamage : PlanetStateFanction
{
    [Header("�v���C���[�Q��")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [Header("�_���[�W����")]
    [SerializeField] private float distance;

    [Header("DPS")]
    [SerializeField] private float damage;

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        //�X���b�v�_���[�W
        Damage(player1, deltaTime);
        Damage(player2, deltaTime);
        return PlanetStateMachine.State.Now;
    }

    private void Damage(GameObject pl_, float tdt)
    {
        //���������������ꍇ�͏����Ȃ�
        Vector3 nowDistance = player1.transform.position - this.transform.position;
        if (nowDistance.magnitude > distance) return;

        PlayerStatus ps = pl_.GetComponent<PlayerStatus>();
        ps.Damage(damage * tdt);
    }

}

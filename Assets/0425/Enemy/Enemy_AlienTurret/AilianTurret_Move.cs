using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilianTurret_Move : MonoBehaviour
{
    [SerializeField] AilianTurret_Status ailianTurret_Status;

    [SerializeField] float rotateSpeed;

    GameObject player;
    private void Start()
    {
        rotateSpeed = ailianTurret_Status.GetSpeed();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (ailianTurret_Status.GetState() == AilianTurret_Status.State.Non || ailianTurret_Status.GetState() == AilianTurret_Status.State.Dead)
        { return; }

        // �^�[�Q�b�g�����̃x�N�g�����擾
        Vector3 relativePos = player.transform.position - this.transform.position;
        // �������A��]���ɕϊ�
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        // ���݂̉�]���ƁA�^�[�Q�b�g�����̉�]����⊮����
        transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotateSpeed * Time.deltaTime);

    }
}
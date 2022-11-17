using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilianTurret_Move : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    [SerializeField] AilianTurret_Status ailianTurret_Status;

    [SerializeField] float rotateSpeed;
    private void Start()
    {
        rotateSpeed = ailianTurret_Status.GetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
       
        // �^�[�Q�b�g�����̃x�N�g�����擾
        Vector3 relativePos = targetObject.transform.position - this.transform.position;
        // �������A��]���ɕϊ�
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        // ���݂̉�]���ƁA�^�[�Q�b�g�����̉�]����⊮����
        transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotateSpeed * Time.deltaTime);

    }
}
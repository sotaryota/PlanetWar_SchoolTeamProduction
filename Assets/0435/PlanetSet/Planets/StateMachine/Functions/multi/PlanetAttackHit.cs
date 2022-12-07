using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAttackHit : PlanetStateFanction
{
    [Header("�v���C���[�Ƀq�b�g�����ۂ�")]
    [Header("��������G�t�F�N�g")]
    [SerializeField]
    private GameObject effectPrefab;
    [SerializeField]
    private Vector3 effectSize = Vector3.one;

    [Header("�炵�������ʉ��̏��")]
    public AudioSource source;
    public AudioClip clip;

    public override PlanetStateMachine.State Fanction(float deltaTime)
    {
        //�G�t�F�N�g����
        GameObject go = Instantiate(effectPrefab);
        go.transform.position = this.transform.position;
        go.transform.localScale = effectSize;

        //���ʉ���炷
        if (source)
        {
            source.PlayOneShot(clip);
        }
        else
        {
            print("PlanetAttackHit�FAudioSource�̎Q�Ƃ�����܂���B");
        }

        //���g��j��
        Destroy(gameObject);
        return PlanetStateMachine.State.Destroy;
    }
}

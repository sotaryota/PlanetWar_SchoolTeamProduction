using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rob1_Attack : MonoBehaviour
{
    [Header("��������f����Prefab�ƈʒu")]
    [SerializeField]
    private GameObject planetPrefab;
    [SerializeField]
    private GameObject planet;
    [SerializeField]
    private GameObject handPos;

    [Header("�X�e�[�^�X�Ǘ��X�N���v�g")]
    public Rob1_Status rob1Status;
    [SerializeField] Rob1_AnimManeger rob1Animator;

    [Header("�f���𓊂������̃E�F�C�g")]
    [SerializeField]
    private float waitTime;
    public bool throwFlag = true;

    float attackCount;
    [SerializeField]
    int attackInterval;

    float attackDelay;
    bool attackGenerate = true;

    [SerializeField]
    public bool sensing = false;

    // Update is called once per frame
    void Update()
    {
        if (rob1Status.GetState() == Rob1_Status.State.Non || rob1Status.GetState() == Rob1_Status.State.Dead)
        { return; }

            if (planet)
        {
            attackCount += Time.deltaTime;

            //�������Ă���f���̈ʒu��r�̈ʒu��
            planet.transform.position = handPos.transform.position;

            if (attackCount > attackInterval)
            {
                PlanetThrow();
            }
        }
        else
        {
            attackCount = 0;
        }
    }

    //--------------------------------------
    //�f���𓊂��鏈��
    //--------------------------------------

    private void PlanetThrow()
    {
        //�f�����������Ă��Ȃ��Ȃ珈�������Ȃ�
        if (!planet) return;


        PlanetStateMachine stateMachine = planet.GetComponent<PlanetStateMachine>();
        PlanetThrowMove throwMove = planet.GetComponent<PlanetThrowMove>();

        //�f���̏�Ԃ�Throw�ɂ���
        stateMachine.SetState(PlanetStateMachine.State.Throw);

        //�f���̃X�s�[�h�Ɣ�ԕ��������߂�
        Vector3 throwSpeed = new Vector3(0, 0, 10);
        Vector3 playerAngle = this.transform.rotation.eulerAngles;
        Vector3 throwAngle = new Vector3(0, playerAngle.y, 0);
        throwMove.ThrowMoveSetting(throwSpeed, throwAngle);

        //������{�C�X�Đ�
        //this.GetComponent<PlayerSEManager>().ThrowVoice();

        //�ϐ�����ɂ��Ĕ�΂�
        planet = null;

        //�A�j���[�V����
        rob1Animator.PlayRob1AnimThrow();

        //�d������
        StartCoroutine("ThrowWait");

    }

    //--------------------------------------
    //���������̍d������
    //--------------------------------------

    IEnumerator ThrowWait()
    {
        throwFlag = false;

        yield return new WaitForSeconds(waitTime);

        //�G�l�~�[��Stay��Ԃɂ���
        rob1Status.SetState(Rob1_Status.State.Stay);

        throwFlag = true;
    }

    //--------------------------------------
    //�U���p�f���̐���
    //--------------------------------------

    private void OnTriggerStay(Collider other)
    {
        if (rob1Status.GetState() == Rob1_Status.State.Non || rob1Status.GetState() == Rob1_Status.State.Dead)
        { return; }
        if (other.tag != "Player")
        { return; }

        sensing = true;

        //�d�����Ԓ��͏��������Ȃ�
        if (!throwFlag) return;

        //�f�����������Ă���ꍇ�͏��������Ȃ�
        if (planet) { return; }

        attackDelay += Time.deltaTime;

        //�v���C����Catch��Ԃ�
        rob1Status.SetState(Rob1_Status.State.Catch);

        //�A�j���[�V����
        if (attackGenerate)
        {
            rob1Animator.PlayRob1AnimGeneration();
            attackGenerate = false;
        }
        

        if (attackDelay > 3)
        {
            //�f���̐���
            GameObject go = Instantiate(planetPrefab);
            //�f��������
            go.transform.position = handPos.transform.position;
            planet = go;
            //�L����
            go.SetActive(true);
            //�v���l�b�g�̏�Ԃ�Catch��Ԃɂ���           
            go.GetComponent<PlanetStateMachine>().SetState(PlanetStateMachine.State.Catch);

            attackDelay = 0;
            attackGenerate = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            sensing = false;
    }
}

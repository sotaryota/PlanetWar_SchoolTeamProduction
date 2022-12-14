using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("�{�X���g��HP���Q��"), SerializeField]
    private Enemy_HpData HPData;

    [Header("�{�X�̃A�j���[�^�[���Q��"), SerializeField]
    private Animator bossAnimator;

    [Header("�v���C���[�Q��"), SerializeField]
    private GameObject player;

    private string nowCoroutine;


    [System.Serializable]
    public class SmashData
    {
        [Tooltip("�����ӏ�")]
        public Transform createPos;

        [Tooltip("���[�V������")]
        public string animTriggerName;

        [Header("�U���̊J�n���ԂƏI������")]
        public float startWait;
        public float endWait;
    }
    [Header("����U�����")]
    [SerializeField] private GameObject smashAttack;
    [SerializeField] private SmashData[] smashData = new SmashData[1];

    [Header("�u���X�U�����")]
    [SerializeField] private Transform breathTargetPos;
    [SerializeField] private GameObject breathPrefab;
    [SerializeField] private Transform createPosObject;
    [SerializeField] private float breathCreateWait;
    [SerializeField] private float breathEndWait;


    private bool attack;//�U��
    private bool looking;//�����킹
    private bool dieAnimPlayed;//���S�A�j���[�V����

    // Start is called before the first frame update
    void Start()
    {
        looking = true;
        attack = false;
        dieAnimPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!BossDie())
        {
            //�A�^�b�N�t���O��true�Ȃ�U���Afalse�Ȃ玲���킹
            if (attack)
            {
                int sttackSelect = Random.Range(0, 2);
                switch (sttackSelect)
                {
                    case 0:
                        nowCoroutine = "Attack_Smash";
                        break;

                    case 1:
                        nowCoroutine = "Attack_Breath";
                        break;

                    default:
                        print("�U���Ȃ�");
                        break;
                }
                StartCoroutine(nowCoroutine);
                attack = false;
            }
            else if(looking)
            {
                //��
                Vector3 lookPos = player.transform.position;
                lookPos.y = this.transform.position.y;
                this.transform.LookAt(lookPos);

                attack = true;
                looking = false;
            }
        }
    }

    IEnumerator Attack_Smash()
    {
        //����������_���擾
        int smashNum = Random.Range(0, smashData.Length);

        //�A�j���[�V����������ɂ��đҋ@
        bossAnimator.SetTrigger(smashData[smashNum].animTriggerName);
        yield return new WaitForSeconds(smashData[smashNum].startWait);

        //�U���𐶐����đҋ@
        GameObject go = Instantiate(smashAttack);
        go.transform.position = smashData[smashNum].createPos.position;
        yield return new WaitForSeconds(smashData[smashNum].endWait);

        //�����킹�L����
        looking = true;

        //�U���I��
        nowCoroutine = null;
    }

    IEnumerator Attack_Breath()
    {
        //�A�j���[�V�������u���X�U���ɂ��đҋ@
        bossAnimator.SetTrigger("Breath");
        yield return new WaitForSeconds(breathCreateWait);

        //�U���𐶐����đҋ@
        GameObject go = Instantiate(breathPrefab);
        go.transform.position = createPosObject.position;
        go.GetComponent<CrabBreathController>().SetTarget(breathTargetPos.position);
        yield return new WaitForSeconds(breathEndWait);

        //�����킹�L����
        looking = true;

        //�U���I��
        nowCoroutine = null;
    }

    public bool BossDie()
    {
        if (HPData.JudgeDie() && !dieAnimPlayed)
        {
            //���݂̃R���[�`�����~
            if (nowCoroutine != null)
            {
                StopCoroutine(nowCoroutine);
            }

            //���S�A�j���[�V����
            bossAnimator.SetTrigger("Die");
            dieAnimPlayed = true;
        }

        //���S���Ă��邩��Ԃ�
        return HPData.JudgeDie();
    }

}

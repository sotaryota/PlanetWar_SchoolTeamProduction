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
    private Vector3 lockPos;

    [Header("�����킹���x�ƍ��킹�鎞��")]
    [SerializeField] private float lookingTime;
    [SerializeField] private float lookingSpeed;

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
    private int smashDataSelect = 0;

    [Header("�u���X�U�����")]
    [SerializeField] private Transform breathTargetPos;
    [SerializeField] private GameObject breathPrefab;
    [SerializeField] private Transform createPosObject;
    [SerializeField] private float breathCreateWait;
    [SerializeField] private float breathEndWait;

    private string nowCoroutine; //���݂̃R���[�`����
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
        if (!BossDie()) {

            if (attack)
            {
                StartCoroutine("Sarch");
                if (nowCoroutine != null)
                {
                    StartCoroutine(nowCoroutine);
                    attack = false;
                }
                
            }
            else if (looking)
            {
                nowCoroutine = "Looking";
                StartCoroutine(nowCoroutine);
            }
        }
    }

    IEnumerator Sarch()
    {
        //�R���[�`�������w�肷��B�w�肵�����
        int attackSelect = Random.Range(0, 6);
        attackSelect /= 5;

        //�T�[�`�G���A�����̓s�x�ύX����
        CrabAttackSearch searchScript;//�ώ�

        for (int i = 0; i < smashData.Length; ++i)
        {
            if (searchScript = smashData[i].createPos.GetComponent<CrabAttackSearch>())
            {
                if(JudgeHitAndStart(searchScript, "Attack_Smash"))
                {
                    smashDataSelect = i;
                    yield break;
                }
            }
        }
        
        if(searchScript = breathTargetPos.GetComponent<CrabAttackSearch>())
        {
            if (searchScript.HitCheck())
            {
                if(JudgeHitAndStart(searchScript, "Attack_Breath"))
                {
                    yield break;
                }
            }
        }

        //�����܂ŗ����珈���Ȃ�
        nowCoroutine = null;
        print("�U���Ȃ�");
        yield break;
    }

    public bool JudgeHitAndStart(CrabAttackSearch searchScript, string coroutineName)
    {
        if (searchScript.HitCheck())
        {
            nowCoroutine = coroutineName;
            return true;
        }
        return false;
    }

    //�����킹
    IEnumerator Looking()
    {
        //�����킹����
        looking = false;

        //�w��t���[�������ēG�̕���������
        for (float i = 0; i < lookingTime; i += Time.deltaTime)
        {
            //��]�x�N�g�����v�Z
            Vector3 myPos = this.transform.position;
            Vector3 pPos = player.transform.position;
            pPos.y = myPos.y;

            //��]�ʂ��v�Z
            Vector3 lookVec = pPos - myPos;
            Quaternion quaternion = Quaternion.LookRotation(lookVec);

            //��]���x���v�Z���Ă����]
            float lookingNowCount = Time.deltaTime * lookingSpeed;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, quaternion, lookingNowCount);
            yield return null;
        }

        //�U���J�n
        attack = true;
    }

    IEnumerator Attack_Smash()
    {
        //�A�j���[�V����������ɂ��đҋ@
        bossAnimator.SetTrigger(smashData[smashDataSelect].animTriggerName);
        yield return new WaitForSeconds(smashData[smashDataSelect].startWait);

        //�U���𐶐����đҋ@
        GameObject go = Instantiate(smashAttack);
        go.transform.position = smashData[smashDataSelect].createPos.position;
        yield return new WaitForSeconds(smashData[smashDataSelect].endWait);

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



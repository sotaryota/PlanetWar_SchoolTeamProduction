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

    [Header("�o�ꎞ�̋��ԍs���̏��")]
    [SerializeField] private float shoutStartWait;
    [SerializeField] private float shoutEndWait;

    [System.Serializable]
    public class SmashData
    {
        [Header("�����ӏ��ƌx���ʒu")]
        public Transform createPos;
        public GameObject warningObject;

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
    [SerializeField] private Transform breathCreatePos;
    [SerializeField] private GameObject breathWarningArea;
    [SerializeField] private float breathCreateWait;
    [SerializeField] private float breathEndWait;
    [Tooltip("�U���m��"), Range(0.0f, 100.0f)]
    [SerializeField] private float breathProbability;

    [Header("���˂��U�����")]
    [SerializeField] private Transform headAttackPos;
    [SerializeField] private GameObject headAttackPrefab;
    [SerializeField] private GameObject HeadWarningArea;
    [SerializeField] private float headCreateWait;
    [SerializeField] private float headEndWait;
    [Tooltip("�U���m��"), Range(0.0f, 100.0f)]
    [SerializeField] private float headProbability;

    [Header("���S���̍s��")]
    [SerializeField] private float dieFallSpeed;

    private string nowCoroutine; //���݂̃R���[�`����
    private bool attack;//�U��
    private bool looking;//�����킹
    private bool dieAnimPlayed;//���S�A�j���[�V����

    // Start is called before the first frame update
    void Start()
    {
        //�t���O�̏����ݒ�
        looking = false;
        attack = false;
        dieAnimPlayed = false;

        //�x���͈͖�����
        breathWarningArea.SetActive(false);

        //����
        StartCoroutine("Shout");
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

    IEnumerator Shout()
    {
        //���юn�߂܂őҋ@
        yield return new WaitForSeconds(shoutStartWait);

        //�A�j���[�V�����ύX
        bossAnimator.SetTrigger("Shout");

        //���яI���܂őҋ@
        yield return new WaitForSeconds(shoutEndWait);

        //�����킹�J�n
        looking = true;
    }

    IEnumerator Sarch()
    {
        //�R���[�`�������w�肷��B
        int attackSelect = Random.Range(0, 6);
        attackSelect /= 5;

        //�m���œ���(�u���X)
        if(Random.Range(0.0f, 100.0f) <= breathProbability)
        {
            nowCoroutine = "Attack_Breath";
            yield break;
        }

        //�m���œ���(���˂�)
        if(Random.Range(0.0f, 100.0f) <= headProbability)
        {
            nowCoroutine = "Attack_Head";
            yield break;
        }

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
        smashData[smashDataSelect].warningObject.SetActive(true);
        yield return new WaitForSeconds(smashData[smashDataSelect].startWait);

        //�U���𐶐����đҋ@
        GameObject go = Instantiate(smashAttack);
        go.transform.position = smashData[smashDataSelect].createPos.position;
        smashData[smashDataSelect].warningObject.SetActive(false);
        yield return new WaitForSeconds(smashData[smashDataSelect].endWait);

        //�����킹�L����
        looking = true;

        //�U���I��
        nowCoroutine = null;
    }

    IEnumerator Attack_Breath()
    {
        //�A�j���[�V�������u���X�U��
        bossAnimator.SetTrigger("Breath");

        //�x���͈͗L����
        breathWarningArea.SetActive(true);

        //�����܂őҋ@
        yield return new WaitForSeconds(breathCreateWait);

        //�U���𐶐����đҋ@
        GameObject go = Instantiate(breathPrefab);
        go.transform.position = breathCreatePos.position;
        go.GetComponent<CrabBreathController>().SetTarget(breathTargetPos.position);

        //�I���܂őҋ@
        for(float time = 0; time < breathEndWait; time += Time.deltaTime)
        {
            //�x���͈͖�����
            if (go == null)
            {
                breathWarningArea.SetActive(false);
            }

            yield return null;
        }

        //�����킹�L����
        looking = true;

        //�U���I��
        nowCoroutine = null;
    }

    IEnumerator Attack_Head()
    {
        //�A�j���[�V�����𓪓˂��U���ɂ��đҋ@
        bossAnimator.SetTrigger("Attack_Head");
        HeadWarningArea.SetActive(true);
        yield return new WaitForSeconds(headCreateWait);

        //�U���𐶐����đҋ@
        GameObject go = Instantiate(headAttackPrefab);
        go.transform.position = headAttackPos.position;
        go.transform.rotation = headAttackPos.rotation;
        HeadWarningArea.SetActive(false);
        yield return new WaitForSeconds(headEndWait);

        //�����킹�L����
        looking = true;

        //�U���I��
        nowCoroutine = null;
    }
    public bool BossDie()
    {
        if (HPData.JudgeDie())
        {
            if (!dieAnimPlayed)
            {
                //���݂̃R���[�`�����~
                if (nowCoroutine != null)
                {
                    StopCoroutine(nowCoroutine);
                }

                //���낢�떳����
                breathWarningArea.SetActive(false);

                //���S�A�j���[�V����
                bossAnimator.SetTrigger("Die");
                dieAnimPlayed = true;
            }

            //���ɒ���
            this.transform.Translate(new Vector3(0, -dieFallSpeed * Time.deltaTime, 0));
        }



        //���S���Ă��邩��Ԃ�
        return HPData.JudgeDie();
    }
}



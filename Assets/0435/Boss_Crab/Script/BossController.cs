using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("�{�X���g��HP���Q��"), SerializeField]
    private Enemy_HpData HPData;

    [Header("�{�X�̃A�j���[�^�[���Q��"), SerializeField]
    private Animator bossAnimator;


    [Header("�u���X�U�����")]
    [SerializeField] private GameObject breathPrefab;
    [SerializeField] private Transform createPosObject;
    [SerializeField] private float breathCreateWait;
    [SerializeField] private float breathEndWait;


    private bool attacked;
    private bool dieAnimPlayed;

    // Start is called before the first frame update
    void Start()
    {
        attacked = false;
        dieAnimPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!BossDie())
        {
            if (!attacked)
            {
                StartCoroutine("Attack_Breath");
                attacked = true;
            }
        }
    }

    IEnumerator Attack_Breath()
    {
        //�A�j���[�V�������u���X�U���ɂ��đҋ@
        bossAnimator.SetTrigger("Breath");
        yield return new WaitForSeconds(breathCreateWait);

        //�U���𐶐����đҋ@
        GameObject go = Instantiate(breathPrefab);
        go.transform.position = createPosObject.position;
        yield return new WaitForSeconds(breathEndWait);

        //�U���I��
        attacked = false;
    }

    public bool BossDie()
    {
        if (HPData.JudgeDie() && !dieAnimPlayed)
        {
            bossAnimator.SetTrigger("Die");
            dieAnimPlayed = true;
        }

        //���S���Ă��邩��Ԃ�
        return HPData.JudgeDie();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBreathController : MonoBehaviour
{
    [Header("�؋󂷂鎞��"), SerializeField]
    private float stayTime;

    [Header("�ڕW���W�ƒe�ۑ��x"), SerializeField]
    private Vector3 targetPos;//�I�����W
    private Vector3 startPos;//�J�n���W

    public void SetTarget(Vector3 pos)
    {
        targetPos = pos;
    }
    [SerializeField] private float speed;//�⊮�l
    float nowCountPos;

    [Header("���e���ɐ�������Prefab"), SerializeField]
    private GameObject prefab;

    [Header("���ʉ��֌W")]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip createClip;
    private bool soundPlay;

    private void Start()
    {
        startPos = this.transform.position;
        nowCountPos = 0;
        source.PlayOneShot(createClip);
        soundPlay = true;
    }

    private void Update()
    {
        stayTime -= Time.deltaTime;
        if(stayTime <= 0)
        {
            if (soundPlay)
            {
                soundPlay = false;
                source.loop = true;
                source.Play();
            }

            nowCountPos += speed * Time.deltaTime;
            this.transform.position = Vector3.Lerp(startPos, targetPos, nowCountPos);
            if(nowCountPos >= 1)
            {
                GameObject go = Instantiate(prefab);
                go.transform.position = this.transform.position;
                Destroy(gameObject);
            }
        }
    }
}

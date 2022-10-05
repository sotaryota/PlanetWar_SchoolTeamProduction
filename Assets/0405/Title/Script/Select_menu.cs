using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;//

public class Select_menu : MonoBehaviour
{
    public Button start;
    public Button end;

    AudioSource audioSource;
    public AudioClip selectSound;

    public GameObject selectedButton;//�I�𒆂̃{�^�����i�[
    GameObject prevButton;

    void Start()
    {
        // �{�^���R���|�[�l���g�̎擾
        start = GameObject.Find("/Canvas/start").GetComponent<Button>();
        end = GameObject.Find("/Canvas/end").GetComponent<Button>();
        //�I�[�f�B�I�\�[�X�̎擾
        audioSource = GetComponent<AudioSource>();
        // �ŏ��ɑI����Ԃɂ������{�^���̐ݒ�
        start.Select();
    }

    void Update()
    {       
        if (selectedButton != EventSystem.current.currentSelectedGameObject)
        {
            //�I�𒆂̂��̂��ς�����特��炷
            selectedButton = EventSystem.current.currentSelectedGameObject;
            //���O�̑I����ۑ�
            prevButton = selectedButton;
            //���ʉ��Đ�
            audioSource.PlayOneShot(selectSound);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

//�|�[�Y���������V�[���ɂ����u���B
//�t�F�[�h����A�{�^���ɂ������摜�A���̃{�^�����܂Ƃ߂�I�u�W�F�N�g�����A�^�b�`����΂���
//�{�^���ɂ̓A�j���[�V�������������邱�ƁB�Ȃ��A�uUpdateMode�v���uUnscaledTime�v�ɂ��邱�ƁI
//���̃V�[���̃}�l�[�W���[��Update�ɉ��L��u���Ƃ��Ȃ��Ɠ������肷��̂Œ���

//PauseMenuSystem pauseMenuSystem;
//if (pauseMenuSystem.pausejudge()) { }

//SetCanPause(_bool_); �Ń|�[�Y�s�ɂł���B
//��b���n�܂�Ƃ���False�𑗂�A�I�������True�𑗂铙

public class PauseSystem_Boss : PauseMenuSystem
{
    //�N���b�N�����Ƃ��̋���
    protected override void onClickAction()
    {
        //�A�Ŗh�~�p
        if (!onButton)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                audioSource.PlayOneShot(pushSound);
                switch (buttonClass[nowSelecting].menuCell)
                {
                    case menu.resume:
                        PausePanel.SetActive(false);
                        Time.timeScale = 1.0f;
                        ispauseNow = false;
                        break;
                    case menu.retry:
                        selectLock = true;
                        onButton = true;
                        fadeScript.fademode = true;
                        Time.timeScale = 1.0f;
                        nextscene = "StoryBoss";
                        break;
                    case menu.back:
                        selectLock = true;
                        onButton = true;
                        fadeScript.fademode = true;
                        Time.timeScale = 1.0f;
                        nextscene = "Story";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

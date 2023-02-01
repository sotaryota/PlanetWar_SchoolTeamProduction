using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SkipOP : MonoBehaviour
{
    Gamepad gamepad;

    [SerializeField] GameObject skipDisplay;

    bool readySkip = false;
    float deleatTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        readySkip = false;
        skipDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gamepad == null) { gamepad = Gamepad.current; }

        //�X�L�b�v������ԂȂ烀�[�r�[�X�L�b�v(���̃V�[�������[�h)
        if (gamepad.buttonEast.wasPressedThisFrame && readySkip == true)
        {
            SceneManager.LoadScene("Story");
        }

        //�X�L�b�v������Ԃ�
        if (gamepad.buttonEast.wasPressedThisFrame && readySkip == false)
        {
            readySkip = true;
        }


        if (readySkip == true)
        {
            skipDisplay.SetActive(true);
            deleatTime += Time.deltaTime;
        }
        else
        {
            skipDisplay.SetActive(false);
            deleatTime = 0;
        }

        if(deleatTime > 5)
        {
            readySkip = false;
        }

    }
}

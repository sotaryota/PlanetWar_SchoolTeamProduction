using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    Gamepad gamepad;
    [SerializeField]
    private MenuManager menuManager;
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private Quaternion cameraRotate;
    public bool buttonLock;

    private void Awake()
    {
        buttonLock = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(gamepad == null)
        {
            gamepad = Gamepad.current;
        }
        
        CameraRotation();
    }

    public void CameraRotation()
    {
        if (buttonLock)
        {
            if (gamepad.leftStick.ReadValue().x > 0)
            {
                StartCoroutine("RightRotation");
            }
            else if (gamepad.leftStick.ReadValue().x < 0)
            {
                StartCoroutine("LeftRotation");
            }
        }
    }
    [SerializeField]
    private float rotateTime;
    IEnumerator RightRotation()
    {
        buttonLock = false;
        if (menuManager.nowSelect >= MenuManager.SelectMenu.End)
        {
            menuManager.nowSelect = MenuManager.SelectMenu.Start;
        }
        else
        {
            menuManager.nowSelect += 1;
        }
        menuManager.menuDatas[(int)menuManager.beforeSelect].menuImage.SetActive(false);
        for (int i = 0; i < 90; ++i)
        {
            mainCamera.transform.Rotate(0,1,0);
            yield return new WaitForSeconds(rotateTime / 90);
        }
        menuManager.menuDatas[(int)menuManager.nowSelect].menuImage.SetActive(true);
        menuManager.beforeSelect = menuManager.nowSelect;
        buttonLock = true;
    }
    IEnumerator LeftRotation()
    {
        buttonLock = false;
        if (menuManager.nowSelect <= MenuManager.SelectMenu.Start)
        {
            menuManager.nowSelect = MenuManager.SelectMenu.End;
        }
        else
        {
            menuManager.nowSelect -= 1;
        }
        menuManager.menuDatas[(int)menuManager.beforeSelect].menuImage.SetActive(false);
        for (int i = 0; i < 90; ++i)
        {
            mainCamera.transform.Rotate(0, -1, 0);
            yield return new WaitForSeconds(rotateTime / 90);
        }
        menuManager.menuDatas[(int)menuManager.nowSelect].menuImage.SetActive(true);
        menuManager.beforeSelect = menuManager.nowSelect;
        buttonLock = true;
    }
}

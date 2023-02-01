using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SkipED : MonoBehaviour
{
    Gamepad gamepad;

    [SerializeField] GameObject skipDisplay;
    [SerializeField] Animator animator;

    bool readySkip = false;
    float deleatTime = 0;

    [SerializeField] GameObject fade;

    [SerializeField] bool staffrollSkip = false;
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


        if (gamepad.buttonEast.wasPressedThisFrame && readySkip == true)
        {
            if(!staffrollSkip)
            {
                animator.SetTrigger("skip");
                fade.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            }
            else
            {
                SceneManager.LoadScene("StoryMenu");
            }   
        }


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

        if (deleatTime > 5)
        {
            readySkip = false;
        }

    }
}

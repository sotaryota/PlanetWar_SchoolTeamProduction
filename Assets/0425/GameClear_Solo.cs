using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear_Solo : MonoBehaviour
{
    [SerializeField] NPCDataManager npcData;
    [SerializeField] PlayerDataManager playerData;
    [SerializeField] EnemyCreater_Start enemyCreater_Start;
    [SerializeField] GameObject finishText;
    [SerializeField] PauseMenuSystem pms;

    [SerializeField] GameObject clearCamPos;
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject playerPos;
    [SerializeField] Animator playerAnim;
    [SerializeField] PlayerCamera_Solo playerCamera_Solo;
    [SerializeField] PlayerMove_Solo playerMove_Solo;
    [SerializeField] PlayerJump playerJump;

    [Header("オーディオ")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip winJingle;
    [SerializeField] PlayerSEManager playerSE;
    [Header("フェード")]
    [SerializeField] private FadeManager fade;   // フェード
    [SerializeField] private Color fadeColor;    // フェードのカラー
    [SerializeField] private float fadeSpeed;    // フェードの速さ
    [SerializeField] private float fadeWait;     // フェードするまでのウェイト
    private bool win;//勝ち負け判定

    // Start is called before the first frame update
    void Start()
    {
        npcData    = GameObject.Find("DataManager").GetComponent<NPCDataManager>();
        playerData = GameObject.Find("DataManager").GetComponent<PlayerDataManager>();
        win = false;
        finishText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCreater_Start.createPrefab) { return; }

        playerCamera.transform.position = clearCamPos.transform.position;
        

        if (!win) {
            playerMove_Solo.enabled = false;
            playerCamera_Solo.enabled = false;
            playerJump.enabled = false;

            playerCamera.transform.LookAt(playerPos.transform.position);
            finishText.SetActive(true);
            playerAnim.SetTrigger("win_solo");
            audioSource.PlayOneShot(winJingle);
            playerSE.WinVoice();
            pms.SetCanPause(false);
            npcData.BattleEndData();
            StartCoroutine("winFade");
            win = true;
        }
    }
    IEnumerator winFade()
    {
        yield return new WaitForSeconds(fadeWait);
        fade.FadeSceneChange("Story", fadeColor.r, fadeColor.g, fadeColor.b, fadeSpeed);
    }
}

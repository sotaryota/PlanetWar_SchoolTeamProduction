using UnityEngine;

/// <summary>
/// ストーリー復帰時にプレイヤーとカメラの位置をバトル移行前の位置に戻す処理
/// </summary>
public class BattleToStory : MonoBehaviour
{
    [SerializeField] PlayerDataManager playerData;
    [SerializeField] FadeManager fadeManager;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerCamera;
    [SerializeField] private float fadeInSpeed;     // フェードのスピード
    [SerializeField] private float fadeInInterval;  // フェード開始までのインターバル
    public bool continueFlag = false;
    public bool toStoryFlag = false;

    private void OnEnable()
    {
        if (!continueFlag)
        {
            playerData = GameObject.Find("DataManager").GetComponent<PlayerDataManager>();
            Vector3 playerPos = Vector3.zero;
            Quaternion playerAngle = Quaternion.Euler(0, 0, 0);
            Quaternion cameraAngle = Quaternion.Euler(0, 0, 0);
            playerData.StoryStartPlayerPos(ref playerPos, ref playerAngle, ref cameraAngle);
            playerCamera.transform.rotation = cameraAngle;
            fadeManager.SceneFadeIn(0.0f, 0.0f, 0.0f, fadeInSpeed, fadeInInterval);
            toStoryFlag = true;
        }
    }
}

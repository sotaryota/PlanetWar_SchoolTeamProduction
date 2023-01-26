using UnityEngine;

/// <summary>
/// ストーリー復帰時にプレイヤーとカメラの位置をバトル移行前の位置に戻す処理
/// </summary>
public class BattleToStory : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerCamera;
    [SerializeField] PlayerDataManager playerData;
    public bool continueFlag = false;

    private void OnEnable()
    {
        if (!continueFlag)
        {
            playerData = GameObject.Find("DataManager").GetComponent<PlayerDataManager>();
            Vector3 playerPos = Vector3.zero;
            Quaternion playerAngle = Quaternion.Euler(0, 0, 0);
            Quaternion cameraAngle = Quaternion.Euler(0, 0, 0);
            playerData.StoryStartPlayerPos(ref playerPos, ref playerAngle, ref cameraAngle);
            player.transform.position = playerPos;
            player.transform.rotation = playerAngle;
            playerCamera.transform.rotation = cameraAngle;
        }
    }
}

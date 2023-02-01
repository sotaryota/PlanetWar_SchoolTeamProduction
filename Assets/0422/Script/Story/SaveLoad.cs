using UnityEngine;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerCamera;
    [SerializeField] NPCList  npcList;
    [SerializeField] SaveData saveData;

    private void Awake()
    {
        saveData = new SaveData();
    }
    public void Save()
    {
        saveData.playerPos   = player.transform.position;
        saveData.playerAngle = player.transform.rotation;
        saveData.cameraAngle = playerCamera.transform.rotation;

        // NPCの状態を保存
        for (int i = 0;i < npcList.npcList.Length;++i)
        {
            saveData.npcStateList[i] = npcList.npcList[i].GetState();
        }
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/" + ".savedata.json", json);
    }
    public void Load()
    {
        string filePath = Application.persistentDataPath + "/" + ".savedata.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<SaveData>(json);

            // プレイヤーをセーブ位置に復帰
            player.transform.position = saveData.playerPos;
            player.transform.rotation = saveData.playerAngle;
            playerCamera.transform.rotation = saveData.cameraAngle;
            // NPCの状態を復元                                   
            for (int i = 0; i < npcList.npcList.Length; ++i)
            {
                npcList.npcList[i].SetState(saveData.npcStateList[i]);
            }
        }
    }
}


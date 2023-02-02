using UnityEngine;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] NPCList  npcList;
    [SerializeField] SaveData saveData;

    private void Awake()
    {
        saveData = new SaveData();
    }
    public void Save()
    {
        // NPC‚Ìó‘Ô‚ğ•Û‘¶
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
            // NPC‚Ìó‘Ô‚ğ•œŒ³                                   
            for (int i = 0; i < npcList.npcList.Length; ++i)
            {
                npcList.npcList[i].SetState(saveData.npcStateList[i]);
            }
        }
    }
}


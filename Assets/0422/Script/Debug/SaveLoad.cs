using UnityEngine;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] NPCList  npcList;
    [SerializeField] SaveData saveData;

    private void Awake()
    {
        saveData = new SaveData();
        npcList = GameObject.Find("NPCStateList").GetComponent<NPCList>();
    }
    public void Save()
    {
        // NPC‚Ìó‘Ô‚ğ•Û‘¶
        for (int i = 0;i < npcList.npcList.Count;++i)
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
            for (int i = 0; i < npcList.npcList.Count; ++i)
            {
                npcList.npcList[i].SetState(saveData.npcStateList[i]);
            }
        }
    }
}


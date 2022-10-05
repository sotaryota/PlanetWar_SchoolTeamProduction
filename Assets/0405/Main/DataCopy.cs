using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCopy : MonoBehaviour
{
    //[System.Serializable]
    public struct PlayerData
    {
        public float HP;
        public float atk;
        public float def;
        public float spd;
    }
    public PlayerData[] playerData = new PlayerData[2];

    [Header("プレイヤー参照")]
    [SerializeField]
    private GameObject player1Object;
    [SerializeField]
    private GameObject player2Object;

    public PlayerStatus ps1;
    public PlayerStatus ps2;
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        ps1 = player1Object.GetComponent<PlayerStatus>();
        ps2 = player2Object.GetComponent<PlayerStatus>();
    }

    void Update()
    {
        if (player1Object)
        {
            playerData[0].HP = ps1.GetHp();
            playerData[0].atk = ps1.GetPower();
            playerData[0].def = ps1.GetDefense();
            playerData[0].spd = ps1.GetSpeed();
        }
        if (player2Object)
        {
            playerData[1].HP = ps2.GetHp();
            playerData[1].atk = ps2.GetPower();
            playerData[1].def = ps2.GetDefense();
            playerData[1].spd = ps2.GetSpeed();
        }
    }
}

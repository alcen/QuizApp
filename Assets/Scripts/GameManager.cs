using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Game data")]
    public GameData data;

    public void SaveData()
    {
        DataManager.SaveData(data);
    }

    public void LoadData()
    {
        data = DataManager.LoadData();
    }
}

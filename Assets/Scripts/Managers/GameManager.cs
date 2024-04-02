using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> enemyTanks = new();
    private List<GameObject> friendTanks = new();
    private int enemyCount; 
    private int friendCount; 

    private int playerXP;

    public event Action<bool> OnGameFinished;
    private void Start()
    {
        LoadPlayerXP();
    }
    public void AddEnemyList(GameObject enemyTank)
    {
        enemyTanks.Add(enemyTank);
        enemyCount = enemyTanks.Count; 
    }
    public List<GameObject> GetEnemyTankList()
    {
        return enemyTanks;
    }
    public void RemoveEnemyTank(GameObject enemyTank)
    {
        enemyTanks.Remove(enemyTank);
        enemyCount = enemyTanks.Count; 
        playerXP += 10;
        if (enemyCount <= 0)
        {
            OnGameFinished?.Invoke(true);
            SavePlayerXP();
        }
    }

    public void AddFriendlyList(GameObject friendTank)
    {
        friendTanks.Add(friendTank);
        friendCount = friendTanks.Count; 
    }

    public List<GameObject> GetFriendTankList()
    {
        return friendTanks;
    }
    public void RemoveFriendTank(GameObject friendTank)
    {
        friendTanks.Remove(friendTank);
        friendCount = friendTanks.Count; 
        if (friendCount <= 0)
        {
            OnGameFinished?.Invoke(false);
            SavePlayerXP();
        }
    }

    public void SavePlayerXP()
    {
        string loadString = SaveManager.Load();
        SaveProps saveProps = JsonUtility.FromJson<SaveProps>(loadString);
        saveProps.playerXP = playerXP;
        string newLoad = JsonUtility.ToJson(saveProps);
        SaveManager.Save(newLoad);
    }
    private void LoadPlayerXP()
    {
        string loadString = SaveManager.Load();
        SaveProps saveProps = JsonUtility.FromJson<SaveProps>(loadString);
        playerXP = saveProps.playerXP;
    }
    public int GetPlayerXP() {
        return playerXP;
    }

}

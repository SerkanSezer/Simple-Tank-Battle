using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GameObject enemyTankPrefab;
    [SerializeField] GameObject friendTankPrefab;

    private const int ENEMY_TANK_COUNT = 6;
    void Start()
    {
        SpawnTanks();
    }

    private void SpawnTanks()
    {
        int enemyCount=0;

        foreach (var item in spawnPoints.ToList()){
            
            NavMeshHit hit;
            if (NavMesh.SamplePosition(item.position, out hit, 10, NavMesh.AllAreas))
            {
                if (enemyCount < ENEMY_TANK_COUNT)
                {
                    Instantiate(enemyTankPrefab, hit.position, enemyTankPrefab.transform.rotation);
                    enemyCount++;
                }
                else
                {
                    Instantiate(friendTankPrefab, hit.position, friendTankPrefab.transform.rotation);
                }
                spawnPoints.Remove(item);
            }            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemySpawner> spawners;
    public List<Enemy> enemies;
    public static MainEnemySpawner Instance;

    [SerializeField] int maxCountEnemy, countDeadEnemies, countEnemiesBeforeBoss;
    [SerializeField] float enemySpawnTime;
    StartBossBattle boss;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        boss = StartBossBattle.Instance;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while(enemies.Count < maxCountEnemy && countDeadEnemies < countEnemiesBeforeBoss)
        {
            yield return new WaitForSeconds(enemySpawnTime);
            while (true)
            {
                int rand = Random.Range(0, spawners.Count);
                if (!spawners[rand].inDeathZone)
                {
                    spawners[rand].SpawnRandomEnemy();
                    break;
                }
            }
        }
    }

    [SerializeField] int bossScene = 2;

    public void EnemyDeath(Enemy enemy)
    {
        enemies.Remove(enemy);
        countDeadEnemies++;
        if(countDeadEnemies == countEnemiesBeforeBoss)
            boss.SpawnPortal(bossScene);
    }
}

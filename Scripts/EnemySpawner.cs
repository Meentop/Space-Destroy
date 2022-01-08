using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector] public bool inDeathZone = false;

    List<GameObject> curStageEnemies;

    [System.Serializable]
    public class EnemyPrefab
    {
        public List<GameObject> enemyPrefab;
    }
    public List<EnemyPrefab> stageEnemies = new List<EnemyPrefab>();

    private void Start()
    {
        curStageEnemies = stageEnemies[PlayerPrefs.GetInt("Stage")].enemyPrefab;
    }

    public void SpawnRandomEnemy()
    {
        if(!inDeathZone)
            Instantiate(curStageEnemies[Random.Range(0, curStageEnemies.Count)], transform.position, Quaternion.identity);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<DeathZone>())
            inDeathZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DeathZone>())
            inDeathZone = false;
    }
}

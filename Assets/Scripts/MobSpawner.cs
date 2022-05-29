using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] GameObject smallEnemyPrefab;
    [SerializeField] GameObject bigEnemyPrefab;
    [SerializeField] float minSpawnTime = 1.5f;
    [SerializeField] float maxSpawnTime = 3f;

    [SerializeField] Vector2[] spawnPoints;
    [SerializeField] int mobsToSpawn = 10;

    Coroutine spawnCoroutine;
    GameObject target;
    GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        gameState = FindObjectOfType<GameState>();
        spawnCoroutine = StartCoroutine(ContinouslySpawnPrefab());
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator ContinouslySpawnPrefab()
    {
        for (int i = 0; i < mobsToSpawn; i++)
        {
            float chanceToSpawnBigEnemy = 0f;
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            int minutesPlayed = (int)gameState.TimePlayed / 60;
            switch (minutesPlayed)
            {
                case 0:
                    chanceToSpawnBigEnemy = 0.1f;

                    break;
                case 1:
                    chanceToSpawnBigEnemy = 0.2f;
                    break;
                case 2:
                    chanceToSpawnBigEnemy = 0.4f;
                    break;
                case 3:
                    chanceToSpawnBigEnemy = 0.6f;
                    break;
                default:
                    chanceToSpawnBigEnemy = 1f;
                    break;
            }

            bool spawnBigEnemy = Random.Range(0f, 1f) < chanceToSpawnBigEnemy;
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            GameObject mobSpawned;

            if (spawnBigEnemy)
                mobSpawned = Instantiate(bigEnemyPrefab, spawnPoints[spawnIndex], Quaternion.identity);
            else
                mobSpawned = Instantiate(smallEnemyPrefab, spawnPoints[spawnIndex], Quaternion.identity);

            mobSpawned.GetComponent<AIController>().SetTarget(target.transform);
            yield return new WaitForSeconds(spawnTime);
        }
    }


    private void OnDrawGizmos()
    {
        if (spawnPoints == null) return;
        foreach (var point in spawnPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(point, 0.2f);
        }
    }
}

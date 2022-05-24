using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] float spawnTime;

    [SerializeField] Vector2[] spawnPoints;

    Coroutine spawnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        spawnCoroutine = StartCoroutine(ContinouslySpawnPrefab());
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator ContinouslySpawnPrefab()
    {
        bool spawning = true;
        int currentIndex = 0;
        while (spawning)
        {
            if (currentIndex >= spawnPoints.Length) currentIndex = 0;

            Instantiate(prefabToSpawn, spawnPoints[currentIndex], Quaternion.identity);
            currentIndex++;
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

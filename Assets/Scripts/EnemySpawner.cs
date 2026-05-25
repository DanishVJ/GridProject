using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private float spawnRate = 1f;
    
    private int _randomIndex;
    private Vector3 _spawnPosition;

    void Start()
    {
        StartCoroutine(TimedEnemySpawner());
    }

    IEnumerator TimedEnemySpawner()
    {
            while (true)
            {
                _randomIndex = Random.Range(0, enemySpawnPoints.Length);

            _spawnPosition = enemySpawnPoints[_randomIndex].position;
            
            Quaternion spawnRotation = enemySpawnPoints[_randomIndex].rotation;

            GameObject spawnedEnemy = Instantiate(enemyPrefab, _spawnPosition, spawnRotation);
            
            Vector3 targetDestination = _spawnPosition + (enemySpawnPoints[_randomIndex].up * 11f);

            // 3. Get the Enemy script component from the prefab and pass it the target destination
            EnemyMovement enemyScript = spawnedEnemy.GetComponent<EnemyMovement>();
            if (enemyScript != null)
            {
                enemyScript.SetTarget(targetDestination);
            }

            yield return new WaitForSeconds(spawnRate);
        }
            
    }
}

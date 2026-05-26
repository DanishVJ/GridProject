using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRate = 15f;      // slower spawn rates
    [SerializeField] private float spawnDelay = 2f;     // initial delay before first spawn

    void Start()
    {
        StartCoroutine(TimedPowerUpSpawner());
    }

    IEnumerator TimedPowerUpSpawner()
    {
        yield return new WaitForSeconds(spawnDelay);

        while (true)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            GameObject spawnedPowerUp = Instantiate(
                powerUpPrefab, spawnPoint.position, spawnPoint.rotation);
            
            Vector3 target = spawnPoint.position + (spawnPoint.up * -11f);

            PowerUp powerUpScript = spawnedPowerUp.GetComponent<PowerUp>();
            if (powerUpScript != null)
                powerUpScript.SetTarget(target);

            yield return new WaitForSeconds(spawnRate);
        }
    }
}

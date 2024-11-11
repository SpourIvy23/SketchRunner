using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject dayObstaclePrefab;    // Obstacle for daytime
    public GameObject nightObstaclePrefab;  // Obstacle for nighttime
    public float minSpawnInterval = 1.5f;
    public float maxSpawnInterval = 3f;
    public float spawnXPosition = 10f;

    private float spawnTimer;

    private void Start()
    {
        spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnObstacle();

            float interval = Mathf.Lerp(maxSpawnInterval, minSpawnInterval, GameManager.Instance.gameSpeed / 20f);
            spawnTimer = Random.Range(interval, maxSpawnInterval);
        }
    }

    private void SpawnObstacle()
    {
        // Check if it is day or night using the Clock instance
        bool isDaytime = Clock.Instance.IsDaytime();

        // Select the correct obstacle prefab based on the time of day
        GameObject obstaclePrefab = isDaytime ? dayObstaclePrefab : nightObstaclePrefab;

        Vector3 spawnPosition = new Vector3(spawnXPosition, transform.position.y, 0);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}

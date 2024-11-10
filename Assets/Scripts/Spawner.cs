using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstaclePrefab; 
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
        
        Vector3 spawnPosition = new Vector3(spawnXPosition, transform.position.y, 0);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}

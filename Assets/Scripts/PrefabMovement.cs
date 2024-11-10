using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private void Update()
    {
        
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;
        
        
        if (transform.position.x < -15) // Adjust -15 to your off-screen limit
        {
            Destroy(gameObject);
        }
    }
}

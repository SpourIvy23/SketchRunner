using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DayObstacle"))
        {
            GameOver("GameOverDay");
        }
        else if (other.CompareTag("NightObstacle"))
        {
            GameOver("GameOverNight");
        }
    }

    private void GameOver(string gameOverScene)
    {
        SceneManager.LoadScene(gameOverScene);
    }
}

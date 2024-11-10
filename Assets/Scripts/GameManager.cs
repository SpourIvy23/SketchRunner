using UnityEngine;

public class GameManager : MonoBehaviour
{
    public  float gameSpeed{get; private set;}

    public float initialGamespeed = 5f;
    public float gameSpeedincrease = 0.1f;
    public static GameManager Instance { get; private set; } 
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            DestroyImmediate(gameObject);
        }
    }   
    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        gameSpeed = initialGamespeed;
    }

    private void Update()
    {
        gameSpeed += gameSpeedincrease * Time.deltaTime;
    }
}

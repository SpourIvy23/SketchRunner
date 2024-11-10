using UnityEngine;

public class GroundMover : MonoBehaviour
{
    public Transform ground1;
    public Transform ground2;
    private float groundWidth;

    private void Start()
    {
        // Calculate the width of the ground segment based on the sprite or object width
        groundWidth = ground1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        // Move both grounds to the left
        ground1.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;
        ground2.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

        // Check if ground1 is completely off-screen
        if (ground1.position.x < -groundWidth)
        {
            // Move ground1 to the right of ground2
            ground1.position = new Vector3(ground2.position.x + groundWidth, ground1.position.y, ground1.position.z);
            SwapGrounds(); // Swap references so ground2 can be repositioned next time
        }
        else if (ground2.position.x < -groundWidth)
        {
            // Move ground2 to the right of ground1
            ground2.position = new Vector3(ground1.position.x + groundWidth, ground2.position.y, ground2.position.z);
            SwapGrounds();
        }
    }

    private void SwapGrounds()
    {
        // Swap references to keep the leapfrogging consistent
        Transform temp = ground1;
        ground1 = ground2;
        ground2 = temp;
    }
}

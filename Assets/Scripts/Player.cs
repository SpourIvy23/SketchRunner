using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController character;
    public Vector3 direction;

    public float gravity = 9.81f * 2;  // Set a reasonable gravity multiplier
    public float jumpForce = 8f;

    private bool isJumping = false;
    private bool wasGrounded = true; // To track if the player was just grounded
    private Animator silverAnimator;

    public void Awake()
    {
        character = GetComponent<CharacterController>();
        silverAnimator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        direction = Vector3.zero;
    }

    void Start()
    {
        // Set initial direction
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Apply gravity
        if (!character.isGrounded)
        {
            direction.y -= gravity * Time.deltaTime;  // Apply gravity only when not grounded
        }
        else
        {
            direction.y = 0f;  // Reset vertical speed when grounded
        }

        // Handle jumping and grounded state
        if (character.isGrounded)
        {
            if (!wasGrounded) // Transition to grounded state if just landed
            {
                wasGrounded = true;
                isJumping = false; // Reset jumping state when we land
                silverAnimator.SetBool("isJumping", false); // Stop jump animation
            }

            // Check for jump input
            if ((Input.GetButton("Jump") || Input.GetMouseButton(0)) && !isJumping)
            {
                // Start jump
                direction.y = jumpForce; // Set upward velocity
                isJumping = true;
                silverAnimator.SetBool("isJumping", true); // Trigger jump animation
            }
        }
        else
        {
            wasGrounded = false; // Player is in the air
        }

        // Move the character
        character.Move(direction * Time.deltaTime);
    }
}
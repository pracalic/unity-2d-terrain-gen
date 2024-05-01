using UnityEditor.Animations;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    private Vector2 movementInput; // Movement input vector
    private Rigidbody2D rb; // Reference to Rigidbody2D component
    [SerializeField] private Animator controller;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get movement input from the horizontal and vertical axes (default keys: WASD or arrow keys)
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Normalize movement input and move the character
        Vector2 normalizedMovement = movementInput.normalized * moveSpeed;
        if (normalizedMovement != Vector2.zero)
        {
            controller.SetBool("move", true);
        }
        else
            controller.SetBool("move", false);
        rb.MovePosition(rb.position + normalizedMovement * Time.fixedDeltaTime);

    }
}

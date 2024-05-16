using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;          
    public float jumpForce = 400f;     
    public Transform groundCheck;      // Check if the character is on the ground
    public LayerMask groundLayer;      // The layer where the ground is located
    public float HorizontalMove = 0f;  

    private Rigidbody2D rb;

    [Header("Player Animation Settings")]
    public Animator animator;          

    [Header("Ground Checker Settings")]
    private bool isGrounded = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        // Detecting keyboard input
        float moveInput = Input.GetAxis("Horizontal");

        // Moving the character left and right
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Checking the direction of movement and rotating the character
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Reflecting horizontally
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Resetting the scale
        }

        // Setting the value of the HorizontalMove parameter in the animator
        animator.SetFloat("HorizontalMove", Mathf.Abs(moveInput));

        animator.SetBool("isGrounded", isGrounded);
    }


    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (Input.GetKeyDown(KeyCode.S) && isGrounded == false)
        {
            rb.AddForce(new Vector2(0f, jumpForce*(-1)));
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }

        // Jump animation
        if (isGrounded == false)
        {
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }
    }
}

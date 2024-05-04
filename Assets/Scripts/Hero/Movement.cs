using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;          // �������� ����������� ���������
    public float jumpForce = 400f;     // ���� ������ ���������
    public Transform groundCheck;      // �����, ��� �����������, �� ����� �� ��������� ��������
    public LayerMask groundLayer;      // ����, �� ������� ��������� �����
    public float HorizontalMove = 0f;  

    private Rigidbody2D rb;

    [Header("Player Animation Settings")]
    public Animator animator;          // ������ �� ��������� Animator

    [Header("Ground Checker Settings")]
    private bool isGrounded = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        // ��������� ���� � ����������
        float moveInput = Input.GetAxis("Horizontal");

        // �������� ��������� ����� � ������
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // �������� ����������� �������� � ������������ ���������
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // ������� �� �����������
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // ��������� �������
        }

        // ����� �������� ��������� HorizontalMove � ���������
        animator.SetFloat("HorizontalMove", Mathf.Abs(moveInput));

        animator.SetBool("isGrounded", isGrounded);
    }


    private void Update()
    {
        // ��������, ��������� �� �� �� �����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // ��������, ������ �� ������� S(����) � ��������� �� �� � �������
        if (Input.GetKeyDown(KeyCode.S) && isGrounded == false)
        {
            // ��������� ���� "������" � ���������, ���������������� ����� - ����
            rb.AddForce(new Vector2(0f, jumpForce*(-1)));
        }

        // ��������, ������ �� ������� ������ � ��������� �� �� �� �����
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // ��������� ���� ������ � ���������
            rb.AddForce(new Vector2(0f, jumpForce));
        }

        // �������� ������
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

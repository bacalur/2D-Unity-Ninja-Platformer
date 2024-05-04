using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;          // Скорость перемещения персонажа
    public float jumpForce = 400f;     // Сила прыжка персонажа
    public Transform groundCheck;      // Точка, где проверяется, на земле ли находится персонаж
    public LayerMask groundLayer;      // Слой, на котором находится земля
    public float HorizontalMove = 0f;  

    private Rigidbody2D rb;

    [Header("Player Animation Settings")]
    public Animator animator;          // Ссылка на компонент Animator

    [Header("Ground Checker Settings")]
    private bool isGrounded = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        // Определяю ввод с клавиатуры
        float moveInput = Input.GetAxis("Horizontal");

        // Движение персонажа влево и вправо
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Проверяю направление движения и поворачиваем персонажа
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Отражаю по горизонтали
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Сбрасываю масштаб
        }

        // Задаю значение параметра HorizontalMove в аниматоре
        animator.SetFloat("HorizontalMove", Mathf.Abs(moveInput));

        animator.SetBool("isGrounded", isGrounded);
    }


    private void Update()
    {
        // Проверяю, находимся ли мы на земле
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Проверяю, нажата ли клавиша S(вниз) и находимся ли мы в воздухе
        if (Input.GetKeyDown(KeyCode.S) && isGrounded == false)
        {
            // Применяем силу "прыжка" к персонажу, перенаправленную назад - вниз
            rb.AddForce(new Vector2(0f, jumpForce*(-1)));
        }

        // Проверяю, нажата ли клавиша прыжка и находимся ли мы на земле
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Применяем силу прыжка к персонажу
            rb.AddForce(new Vector2(0f, jumpForce));
        }

        // Анимация прыжка
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

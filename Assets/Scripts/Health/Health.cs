using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Animator animator;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        LoadHealth(); // �������� ������������ ���������� ������ ��� ������� �����
        UpdateHeartsUI(); // ���������� UI ������
    }

    private void FixedUpdate()
    {
        if (currentHealth > numOfHearts)
        {
            currentHealth = numOfHearts;
        }

        UpdateHeartsUI(); // ���������� UI ������
    }

    public void TakeDemage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // player hurt
            animator.SetTrigger("Hurt");
        }
        else
        {
            // player dead
            animator.SetTrigger("Death");
            SaveHealth(); // ���������� ���������� ������ ����� ������������� �����
            Invoke("ReloadScene", 2f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            TakeDemage(1);
        }
    }

    // �������� ������������ ���������� ������
    void LoadHealth()
    {
        if (PlayerPrefs.HasKey("Health"))
        {
            currentHealth = PlayerPrefs.GetFloat("Health");
        }
        else
        {
            currentHealth = numOfHearts;
        }
    }

    // ���������� ���������� ������
    void SaveHealth()
    {
        PlayerPrefs.SetFloat("Health", currentHealth);
        PlayerPrefs.Save();
    }

    // �������� �����
    void ReloadScene()
    {
        if (currentHealth <= 0)
        {
            LoadMenuScene(); // �������� ����� ����, ���� ��� ����������� ������
        }
        else
        {
            SceneManager.LoadScene(1); // �������� ����� ����
        }
    }

    // �������� ����� ����
    void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    // ���������� UI ������
    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(currentHealth))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}

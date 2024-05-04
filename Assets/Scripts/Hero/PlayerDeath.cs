using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            // Анимация смерти игрока
            animator.SetTrigger("Death");

            // Загрузка сцены после задержки в 2 секунды
            Invoke("ReloadScene", 2f);


        }
    }

    // Загрузка сцены
    void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}

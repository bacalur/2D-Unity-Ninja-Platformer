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
            // Player death animation
            animator.SetTrigger("Death");

            // Loading the scene after a 2-second delay
            Invoke("ReloadScene", 2f);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}

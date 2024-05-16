using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public MeleeEnemy meleeEnemy;


    private void Start()
    {
        meleeEnemy = FindObjectOfType<MeleeEnemy>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Left mouse button - attack
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        // Checking for player collision with an enemy and triggering the enemy death animation
        if (meleeEnemy != null && meleeEnemy.PlayerInSight())
        {
            meleeEnemy.Die();
        }
    }
}

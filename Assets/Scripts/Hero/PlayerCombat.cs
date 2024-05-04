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
        // ЛКМ - атака
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Анимация атаки
        animator.SetTrigger("Attack");

        // Проверка столкновения игрока с врагом и вызов анимации смерти врага
        if (meleeEnemy != null && meleeEnemy.PlayerInSight())
        {
            meleeEnemy.Die();
        }
    }
}

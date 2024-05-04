using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;  // время перезарядки атаки
    [SerializeField] private float range;
    [SerializeField] private int damage;   // урон врага

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;  // расстояние коллайдера
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;  // таймер перезарядки; Mathf.Infinity - чтобы враг мог атаковать сразу

    // Ссылки
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()  // готов ли враг к атаке?
    {
        cooldownTimer += Time.deltaTime;

        // Attack только когда Hero виден
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                // Attack
                cooldownTimer = 0;
                anim.SetTrigger("knightAttack");
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    public bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                                             new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                                             0, Vector2.left, 0, playerLayer);
        // 1. начало координат; 2. размер; 3. угол; 
        // 4. направление; 5. расстояние; 6. маска слоя

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DemagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDemage(damage);
        }
    }

    public void Die()
    {
        anim.SetTrigger("die");

        StartCoroutine(DestroyAfterDelay(0.35f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}

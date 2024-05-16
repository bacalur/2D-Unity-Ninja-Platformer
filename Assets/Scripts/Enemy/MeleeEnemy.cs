using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;  // Attack reload time
    [SerializeField] private float range;
    [SerializeField] private int damage;   // Enemy damage

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;  
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;  // Reload timer; Mathf.Infinity - so that the enemy can attack immediately

    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()  // Is the enemy ready to attack?
    {
        cooldownTimer += Time.deltaTime;

        // Attack only when Hero is visible
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
        // 1. origin; 2. size; 3. angle;
        // 4. direction; 5. distance; 6. layer mask

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

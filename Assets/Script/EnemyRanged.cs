using System;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
  [Header("Attack Parameters")] [SerializeField]
  private float attackCooldown;

  [SerializeField] private float range;
  [SerializeField] private int damage;

  [Header("Collider Parameters")] [SerializeField]
  private float colldierDistance;

  [SerializeField] private BoxCollider2D boxCollider;

  [Header("Player Layer")] [SerializeField]
  private LayerMask playerLayer;

  private float cooldownTimer = Mathf.Infinity;

  private Health playerHealth;

  private EnemyPatroling enemyPatrol;

  private void Awake()
  {
    enemyPatrol = GetComponentInParent<EnemyPatroling>();
  }

  private bool PlayerInSight()
  {
    RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0,
      playerLayer);
    return hit.collider != null;
  }

  private void Update()
  {
    cooldownTimer += Time.deltaTime;

    if (enemyPatrol != null)
      enemyPatrol.enabled = !PlayerInSight();
  }
}

using System;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
  [Header("Attack Parameters")] [SerializeField]
  private float attackCooldown;

  [SerializeField] private float range;
  [SerializeField] private int damage;

  [SerializeField]private GameObject Bullet;
  [SerializeField]private Transform head;

  
  
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
    RaycastHit2D hit = Physics2D.BoxCast(
      boxCollider.bounds.center + transform.right * range * transform.localScale.x * colldierDistance,
      new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0,
      Vector2.left, 0, playerLayer);
    return hit.collider != null;
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colldierDistance,
      new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y,boxCollider.bounds.size.z));
  }


  private void Update()
  {
    cooldownTimer += Time.deltaTime;
    if (PlayerInSight())
    {
      
      if (cooldownTimer >= attackCooldown)
      {
        cooldownTimer = 0;
        Shoot();
      }
    }

    if (enemyPatrol != null)
      enemyPatrol.enabled = !PlayerInSight();
    
  }

  public void Shoot()
  {
    GameObject go = Instantiate(Bullet, head.position, Quaternion.identity);
    Vector3 direction = new Vector3(transform.localScale.x, 0);
    go.GetComponent<Projectile>().Setup(direction);
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      collision.GetComponent<Health>().TakeDamage(damage);
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private Vector2 direction;

    [SerializeField] private string targetTag;

    void Update()
    {
        transform.Translate(direction * speed *Time.deltaTime);
    }

    public void Setup(Vector2 direction)
    {
        this.direction = direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    

}

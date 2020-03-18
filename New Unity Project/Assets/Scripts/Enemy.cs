using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int currentHP;
    public int maxHP;
    public float moveSpeed;

    [Header("Target")]
    public float chaseRange;
    public float attackRange;
    private Player player;

    private Rigidbody2D rigidBody;

    void Awake()
    {
        // aquire target
        player = FindObjectOfType<Player>();
        // aquire enemy body
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);

        if(playerDistance <= attackRange)
        {
            // attack player
            rigidBody.velocity = Vector2.zero;
        }
        else if(playerDistance <= chaseRange)
            ChasePlayer();
        else
            rigidBody.velocity = Vector2.zero;
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rigidBody.velocity = direction * moveSpeed;
    }

    public void TakeDamage(int damageTaken)
    {
        currentHP -= damageTaken;
        if(currentHP <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}

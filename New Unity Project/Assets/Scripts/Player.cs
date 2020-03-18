using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int currentHP;
    public int maxHP;
    public float moveSpeed;
    public int damage;

    [Header("Combat")]
    public float attackRange;
    public float attackRate;
    private float lastAttackTime;
    public KeyCode attackKey;

    [Header("Sprites")]
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    private SpriteRenderer playerRenderer;

    private Vector2 facingDirection;
    //components
    private Rigidbody2D rigidBody;

    void Awake ()
    {
        // get the components
        rigidBody = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update ()
    {
        Move();
        if(Input.GetKeyDown(attackKey))
        {
            if (Time.time - lastAttackTime > attackRate)
                Attack();
        }
    }

    void Attack ()
    {
        lastAttackTime = Time.time;
        RaycastHit2D attack = Physics2D.Raycast(transform.position, facingDirection, attackRange, 1 << 8);
        if(attack.collider != null)
            attack.collider.GetComponent<Enemy>().TakeDamage(damage);
    }

    void Move ()
    {
        // get the horizontal and vertical keyboard inputs
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // calculate the velocity we're going to move at
        Vector2 velocity = new Vector2(x, y);

        // calculate the facing direction
        if (velocity.magnitude != 0)
            facingDirection = velocity;

        ChangePlayerFacingDirection();

        // set the velocity
        rigidBody.velocity = velocity * moveSpeed;
    }

    void ChangePlayerFacingDirection()
    {
        if (facingDirection == Vector2.up)
            playerRenderer.sprite = upSprite;
        else if (facingDirection == Vector2.down)
            playerRenderer.sprite = downSprite;
        else if (facingDirection == Vector2.left)
            playerRenderer.sprite = leftSprite;
        else if (facingDirection == Vector2.right)
            playerRenderer.sprite = rightSprite;
    }
}

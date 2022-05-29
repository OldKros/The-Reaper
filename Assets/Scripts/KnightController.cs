using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float attackSpeed = 2f;
    [SerializeField] GameObject weaponSwinger;

    public int CoinCount { get; private set; } = 0;

    float lastAttack = float.MaxValue;
    bool canAttack = true;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Health health;
    Rigidbody2D rb2D;

    public event Action pickupCoin;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.healthRemoved += PlayHitAnimation;
    }

    // Update is called once per frame
    void Update()
    {
        lastAttack += Time.deltaTime;
        canAttack = lastAttack >= attackSpeed;
        HandleInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Debug.Log("Attacking");
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        lastAttack = 0f;
    }

    void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * moveSpeed;

        // print("X:" + deltaX + ", Y: " + deltaY);
        if (deltaX != 0f || deltaY != 0f)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        bool movingLeft = deltaX < 0;
        Vector3 characterScale = transform.localScale;
        if (!Mathf.Approximately(deltaX, 0f))
        {
            characterScale.x = movingLeft ? -1 : 1;
        }
        transform.localScale = characterScale;


        // float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        // float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        // float newXPos = transform.position.x + deltaX;
        // float newYPos = transform.position.y + deltaY;

        // Vector2 newPosition = new Vector2(newXPos, newYPos);

        rb2D.velocity = new Vector2(deltaX, deltaY);
        // float distance = Vector2.Distance(transform.position, newPosition);
        // animator.SetFloat("MoveSpeed", distance);

        // transform.position = newPosition;
    }

    void PlayHitAnimation()
    {
        animator.SetTrigger("Hit");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickup"))
        {
            print(this.name);
            print(other.name);
            var pickup = other.GetComponent<Pickup>();
            switch (pickup.Type)
            {
                case Pickup.PickupType.Coin:
                    CoinCount += pickup.Value;
                    pickupCoin?.Invoke();
                    break;
                default:
                    break;
            }
        }
    }
}

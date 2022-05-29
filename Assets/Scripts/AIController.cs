using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float stunLength = 2f;

    [SerializeField] GameObject deathEffect;



    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rb2d;
    Health health;


    bool stunned = false;
    float stunTimer = float.MaxValue;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.onDeath += Die;
        health.healthRemoved += StunAI;
    }

    void Update()
    {
        stunTimer += Time.deltaTime;
        stunned = stunTimer < stunLength;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (target == null) return;
        if (stunned) return;

        // var movementThisFrame = moveSpeed * Time.deltaTime;
        // Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, movementThisFrame);
        Vector2 moveVector = target.position - transform.position;
        Vector2 direction = moveVector.normalized;

        // Vector2 velocity = rb2d.velocity;
        // velocity += direction * moveSpeed;

        // Debug.Log(direction);
        // moveVector = direction * moveSpeed * Time.fixedDeltaTime;

        rb2d.velocity = direction * moveSpeed;

        // rb2d.MovePosition(rb2d.position + moveVector);
        float deltaX = target.position.x - transform.position.x;
        float deltaY = target.position.y - transform.position.y;
        // transform.position = newPosition;

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

    }

    private void Die()
    {
        PlayDeathEffect();
        Destroy(gameObject);
    }

    private void PlayDeathEffect()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        float effectTime = effect.GetComponent<ParticleSystem>().main.duration;
        // GetComponent<Collider2D>().enabled = false;
        // spriteRenderer.enabled = false;
        Destroy(effect, effectTime);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void StunAI()
    {
        stunned = true;
        stunTimer = 0.0f;
    }
}

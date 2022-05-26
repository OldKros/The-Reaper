using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float attackSpeed = 2f;
    [SerializeField] GameObject weaponSwinger;

    float lastAttack = float.MaxValue;
    bool canAttack = true;

    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastAttack += Time.deltaTime;
        canAttack = lastAttack >= attackSpeed;
        HandleInput();
    }

    private void HandleInput()
    {
        Move();
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        this.
        lastAttack = 0f;
    }

    void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        print("X:" + deltaX + ", Y: " + deltaY);
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

        float newXPos = transform.position.x + deltaX;
        float newYPos = transform.position.y + deltaY;



        Vector2 newPosition = new Vector2(newXPos, newYPos);
        float distance = Vector2.Distance(transform.position, newPosition);
        // animator.SetFloat("MoveSpeed", distance);

        transform.position = newPosition;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float moveSpeed = 2f;


    SpriteRenderer spriteRenderer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (target == null) return;
        var movementThisFrame = moveSpeed * Time.deltaTime;
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, movementThisFrame);
        float deltaX = newPosition.x - transform.position.x;
        float deltaY = newPosition.y - transform.position.y;
        transform.position = newPosition;

        if (deltaX != 0f || deltaY != 0f)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        // print(deltaX);
        if (!Mathf.Approximately(deltaX, 0f))
        {
            spriteRenderer.flipX = deltaX < 0;
        }

    }
}

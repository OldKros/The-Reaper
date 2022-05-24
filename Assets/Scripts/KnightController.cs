using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;



    Animator animator;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        bool movingLeft = deltaX < 0;

        if (!Mathf.Approximately(deltaX, 0f))
        {
            spriteRenderer.flipX = movingLeft;
        }


        // float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        // float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        float newXPos = transform.position.x + deltaX;
        float newYPos = transform.position.y + deltaY;



        Vector2 newPosition = new Vector2(newXPos, newYPos);
        float distance = Vector2.Distance(transform.position, newPosition);
        animator.SetFloat("MoveSpeed", distance);

        transform.position = newPosition;
    }
}

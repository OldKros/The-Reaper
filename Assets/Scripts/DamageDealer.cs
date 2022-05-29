using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [SerializeField] float knockbackEffect = 2f;
    public bool CanAttack { get; private set; }
    private float timeSinceLastAttack = float.MaxValue;
    private Vector2 knockbackDirection;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        CanAttack = timeSinceLastAttack > AttackSpeed;

        // Debug.Log($"Can AttacK:{CanAttack}, Time: {timeSinceLastAttack}");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Health target = other.GetComponent<Health>();
        DealDamage(target);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Health target = other.GetComponent<Health>();
        DealDamage(target);
    }

    private void DealDamage(Health target)
    {
        if (target == null) return;

        if (CanAttack)
        {
            timeSinceLastAttack = 0.00f;
            target.TakeDamage(this);


            Vector2 knockbackDirection = transform.position.x > target.transform.position.x ? Vector2.left : Vector2.right;
            var knockbackForce = knockbackDirection * knockbackEffect;
            var targetRb = target.GetComponent<Rigidbody2D>();
            if (targetRb != null)
            {
                targetRb.velocity = new Vector2(0, 0);
                targetRb.AddForce(knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDisable()
    {
        timeSinceLastAttack = float.MaxValue;
    }



}

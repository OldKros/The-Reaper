using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    public bool CanAttack { get; private set; }
    private float timeSinceLastAttack = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CanAttack = timeSinceLastAttack > AttackSpeed;
        timeSinceLastAttack += Time.deltaTime;

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
        }
    }





}

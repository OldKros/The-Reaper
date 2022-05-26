using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int MaximumHealth { get; private set; }
    [field: SerializeField] public int CurrentHealth { get; private set; }

    [field: SerializeField]
    [field: Range(0, 100)]
    public float HealthPercent { get; private set; } = 100;

    public event Action healthChanged;




    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaximumHealth;
        healthChanged += UpdateHealthPercent;
        healthChanged?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void TakeDamage(DamageDealer dd)
    {
        CurrentHealth -= dd.Damage;
        healthChanged?.Invoke();
    }


    private void UpdateHealthPercent()
    {
        HealthPercent = ((float)CurrentHealth / (float)MaximumHealth) * 100;
        // return HealthPercent;
    }
}

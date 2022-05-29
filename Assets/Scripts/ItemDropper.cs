using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] Pickup pickupDrop;
    [SerializeField] int pickupAmount;
    [SerializeField][Range(0, 1)] float dropChance;

    Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        health.onDeath += DropItem;
    }

    private void DropItem()
    {
        if (Random.Range(0f, 1f) < dropChance)
        {

            Pickup drop = Instantiate(pickupDrop, transform.position, Quaternion.identity);
        }
    }


}
